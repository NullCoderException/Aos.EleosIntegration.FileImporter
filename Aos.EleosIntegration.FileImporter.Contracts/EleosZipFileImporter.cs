//  COPYRIGHT (C) 2019,  AOS, LLC.  All rights reserved.
//  Duplication in any media is strictly prohibited without
//  explicit written permission from AOS, LLC.
//
//  Author: Chris Thomas <cthomas@aos.biz>
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aos.EleosIntegration.FileImporter.Contracts
{
    public class EleosZipFileImporter : IEleosFileImporter
    {
        private EmailSender _emailSender;

        public void ProcessZipFile(string zipPath)
        {
            // Normalizes the path.
            zipPath = Path.GetFullPath(zipPath);
            var message = InternalProcessZipFile(zipPath);
            _emailSender = new EmailSender();
            _emailSender.SendEmail(message);
        }

        private MailMessage InternalProcessZipFile(string zipPath)
        {
            DriveAxleDocument metadata = null;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(ConfigurationManager.AppSettings["EmailFromAddress"]);

            string[] extensions = new[] { ".pdf", ".tiff", ".jpg", ".png", ".bmp" };
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)) //is an xml metadata file
                    {
                        //found metadata .xml file, parse to object
                        StreamReader reader = new StreamReader(entry.Open());
                        IMetadataParser parser = new XmlMetadataParser();
                        metadata = parser.ParseMetadataFile(reader.ReadToEnd());
                        string loadNumber = metadata.Identifiers.LoadNumber;
                        message.From = new MailAddress(ConfigurationManager.AppSettings["EmailFromAddress"]);
                        message.To.Add("cthomas@aos.biz");
                        if (metadata.CustomProperties?.SCANMODE == "DOCUMENT")
                        {
                            //is a document

                            message.To.Add(ConfigurationManager.AppSettings["DocumentEmailAddress"]);
                            message.Subject = $"load={loadNumber},driver={metadata.SDKUserId}";
                            message.Body = $"This is a document from driver {metadata.SDKUserId}";
                        }
                        else
                        {
                            //common "NOT A DOCUMENT" email building logic
                            message.Subject = $"Accident Report from Driver {metadata.SDKUserId}";

                            message.Body = $"Driver {metadata.SDKUserId} sent an accident report on {metadata.CreatedAt} with the following info: \r\n";
                            message.Body += $"Driver : {metadata.SDKUserId}\r\n";
                            if (!String.IsNullOrEmpty(loadNumber)) message.Body += $"Load Number : {loadNumber}\r\n";

                            PropertyInfo[] properties = metadata.CustomProperties.GetType().GetProperties();

                            foreach (PropertyInfo property in properties)
                            {
                                if (property.GetValue(metadata.CustomProperties) != null && (property.Name != "SCANMODE"))
                                {
                                    message.Body += property.Name + ":  " + property.GetValue(metadata.CustomProperties) + "\r\n";
                                }
                            }

                            //try to append EmailAddress to form type unless form type is null to get destination email address
                            string toEmailSetting = metadata.CustomProperties.FormType + "EmailAddress";
                            if (toEmailSetting == "EmailAddress")
                            {
                                message.To.Add(ConfigurationManager.AppSettings["DefaultEmailAddress"]);
                            }
                            else
                            {
                                message.To.Add(ConfigurationManager.AppSettings[toEmailSetting]);
                            }

                            //if (metadata.CustomProperties.FormType == "22-ACCIDENT")
                            //{
                            //    //Is a custom accident

                            //    //add custom properties to body
                            //}
                            //if (metadata.CustomProperties.FormType == "LoadPics")
                            //{
                            //    //DO STUFF
                            //}
                        }
                    }
                    else if (extensions.Any(x => entry.FullName.EndsWith(x, StringComparison.OrdinalIgnoreCase))) //is an image file
                    {
                        var stream = new MemoryStream();

                        // extract the content from the zip archive entry
                        using (var content = entry.Open())
                            content.CopyTo(stream);

                        // rewind the stream
                        stream.Position = 0;

                        message.Attachments.Add(new Attachment(stream, entry.FullName));
                    }
                }
            }
            return message;
        }

        public List<string> FindZipFilesInDirectory(string dir)
        {
            var ext = new List<string> { ".zip" };
            var myFiles = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories)
                 .Where(s => ext.Contains(Path.GetExtension(s)));
            return myFiles.ToList();
        }
    }
}