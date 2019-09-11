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
using System.Text;
using System.Threading.Tasks;

namespace Aos.EleosIntegration.FileImporter.Contracts
{
    public class EleosZipFileImporter : IEleosFileImporter
    {
        public void ProcessZipFile(string zipPath)
        {
            // Normalizes the path.
            zipPath = Path.GetFullPath(zipPath);
            var message = InternalProcessZipFile(zipPath);
            EmailSender sender = new EmailSender();
            sender.SendEmail(message);
        }

        private MailMessage InternalProcessZipFile(string zipPath)
        {
            DriveAxleDocument metadata = null;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(ConfigurationManager.AppSettings["SendEmailFromAddress"]);

            string[] extensions = new[] { ".pdf", ".tiff", ".jpg", ".png", ".bmp" };
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        //found metadata .xml file
                        StreamReader reader = new StreamReader(entry.Open());
                        IMetadataParser parser = new XmlMetadataParser();
                        metadata = parser.ParseMetadataFile(reader.ReadToEnd());

                        if (metadata.CustomProperties?.SCANMODE == "DOCUMENT")
                        {
                            //is a document

                            message.To.Add(ConfigurationManager.AppSettings["DocumentEmailAddress"]);
                            message.Subject = $"driver={metadata.SDKUserId},order={metadata.Identifiers.LoadNumber}";
                            message.Body = $"This is a document from driver {metadata.SDKUserId}";
                        }
                        else
                        {
                            if (metadata.CustomProperties.FormType == "22-ACCIDENT")
                            {
                                //Is a custom accident
                                message.To.Add(ConfigurationManager.AppSettings["AccidentEmailAddress"]);
                                message.Subject = $"Accident Report from Driver {metadata.SDKUserId}";
                                message.Body = $"Driver {metadata.SDKUserId} sent an accident report on {metadata.CreatedAt} with the following info: ";
                                //add custom properties to body
                            }
                            if (metadata.CustomProperties.FormType == "LoadPics")
                            {
                                //Is a LoadPics form type with no SCANMODE

                                //DO STUFF
                            }
                        }
                    }
                    else if (extensions.Any(x => entry.FullName.EndsWith(x, StringComparison.OrdinalIgnoreCase)))
                    {
                        //found a file attachment image file
                        using (var stream = entry.Open())
                        {
                            message.Attachments.Add(new Attachment(stream, entry.FullName));
                        }
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