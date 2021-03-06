﻿//  COPYRIGHT (C) 2019,  AOS, LLC.  All rights reserved.
//  Duplication in any media is strictly prohibited without
//  explicit written permission from AOS, LLC.
//
//  Author: Chris Thomas <cthomas@aos.biz>
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Mail;

namespace Aos.EleosIntegration.FileImporter.Contracts
{
    public class EleosZipFileImporter : IEleosFileImporter
    {
        private EmailSender _emailSender;

        public void ProcessZipFile(string zipPath)
        {
            // Normalizes the path.

            zipPath = Path.GetFullPath(zipPath);
            Log.Information($"Processing zip file {zipPath}");
            var message = InternalProcessZipFile(zipPath);
            Log.Information($"...Done processing {zipPath}");
            _emailSender = new EmailSender();
            _emailSender.SendEmail(message);
        }

        private MailMessage InternalProcessZipFile(string zipPath)
        {
            DriveAxleDocument metadata = null;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(ConfigurationManager.AppSettings["EmailFromAddress"]);

            string[] extensions = new[] { ".pdf", ".tif", ".jpg", ".png", ".bmp" };
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)) //is an xml metadata file
                    {
                        Log.Information($"Found metadata file {entry.FullName}");

                        //found metadata .xml file, parse to object
                        StreamReader reader = new StreamReader(entry.Open());
                        IMetadataParser parser = new XmlMetadataParser();
                        string xml = reader.ReadToEnd();
                        metadata = parser.ParseMetadataFile(xml);

                        //not great to double parse but not terrible
                        var customProperties = parser.GetCustomProperties(xml);

                        var documentTypes = parser.GetDocumentTypes(xml);

                        string loadNumber = metadata.Identifiers.LoadNumber;

                        message.From = new MailAddress(ConfigurationManager.AppSettings["EmailFromAddress"]);
                        if (metadata.CustomProperties?.SCANMODE == "DOCUMENT")
                        {
                            message.Subject = $"ScanDocs Report from Driver {metadata.SDKUserId}";
                            // Use DocumentType Address if special documentTypes
                            if (documentTypes != null)
                            {
                                string documentTypeAddresses = "";
                                foreach(var value in documentTypes.Values)
                                {
                                    documentTypeAddresses = ConfigurationManager.AppSettings[$"Document{new string(value.Where(x => !char.IsWhiteSpace(x)).ToArray())}EmailAddress"];
                                }

                                if(!string.IsNullOrWhiteSpace(documentTypeAddresses))
                                {
                                    foreach (var address in documentTypeAddresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        message.To.Add(address);
                                    }
                                }
                                else
                                {
                                    //Default Email Address
                                    var addresses = ConfigurationManager.AppSettings["DocumentEmailAddress"];
                                    foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        message.To.Add(address);
                                    }
                                }
                                
                            }
                            else
                            {
                                //Default Email Address
                                var addresses = ConfigurationManager.AppSettings["DocumentEmailAddress"];
                                foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    message.To.Add(address);
                                }

                            }

                           
                        }
                        else if (metadata.CustomProperties?.EMAILADDRESS != null && metadata.CustomProperties.FormType == "Email")
                        {
                            message.To.Add(metadata.CustomProperties.EMAILADDRESS);
                            message.Subject = $"{metadata.CustomProperties.FormType} Report from Driver {metadata.SDKUserId}";
                        }
                        else
                        {
                            //common "NOT A DOCUMENT" email building logic
                            if (metadata.CustomProperties?.FormType == "DOCUMENT-METADATA")
                            {
                                message.Subject = $"ScanDocs Report from Driver {metadata.SDKUserId}";

                                // Use DocumentType Address if special documentTypes
                                if (documentTypes != null)
                                {
                                    string documentTypeAddresses = "";
                                    foreach (var value in documentTypes.Values)
                                    {
                                        documentTypeAddresses = ConfigurationManager.AppSettings[$"Document{new string(value.Where(x => !char.IsWhiteSpace(x)).ToArray())}EmailAddress"];
                                    }

                                    if (!string.IsNullOrWhiteSpace(documentTypeAddresses))
                                    {
                                        foreach (var address in documentTypeAddresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                                        {
                                            message.To.Add(address);
                                        }
                                    }
                                    else 
                                    {
                                        var addresses = ConfigurationManager.AppSettings["DocumentEmailAddress"];
                                        foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                                        {
                                            message.To.Add(address);
                                        }
                                    }

                                }
                                else
                                {
                                    //Default Email Address For Documents
                                    var addresses = ConfigurationManager.AppSettings["DocumentEmailAddress"];
                                    foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        message.To.Add(address);
                                    }
                                }
                            }
                            else
                            {
                                message.Subject = $"{metadata.CustomProperties.FormType} Report from Driver {metadata.SDKUserId}";
                            }

                            //try to append EmailAddress to form type unless form type is null to get destination email address
                            string toEmailSetting = metadata.CustomProperties.FormType + "EmailAddress";

                            if (toEmailSetting == "EmailAddress")
                            {
                                var addresses = ConfigurationManager.AppSettings["DefaultEmailAddress"];
                                foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    message.To.Add(address);
                                }
                            }
                            else
                            {
                                var addresses = ConfigurationManager.AppSettings[toEmailSetting];
                                if (addresses != null)
                                {
                                    foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        message.To.Add(address);
                                    }
                                }
                                else
                                {
                                    if(message.To == null) 
                                    {
                                        var defaultAddresses = ConfigurationManager.AppSettings["DefaultEmailAddress"];
                                        foreach (var address in defaultAddresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                                        {
                                            message.To.Add(address);
                                        }
                                    }
                                    
                                }
                            }
                        }

                        message.Body = $"Driver {metadata.SDKUserId} sent an {metadata.CustomProperties.FormType} report on {metadata.CreatedAt.ToLocalTime()} with the following info: \r\n";
                        message.Body += $"Driver : {metadata.SDKUserId}\r\n";
                        if (!String.IsNullOrEmpty(loadNumber)) message.Body += $"Load Number : {loadNumber}\r\n";

                        //using property dictionary instead of reflection
                        //PropertyInfo[] properties = metadata.CustomProperties.GetType().GetProperties();
                        foreach (var customProperty in customProperties)
                        {
                            if ((customProperty.Value != null) && (customProperty.Key != "SCANMODE") && (customProperty.Key != "SentAt"))
                            {
                                message.Body += customProperty.Key + ": " + customProperty.Value + "\r\n";
                            }
                        }

                        //using property dictionary to print document types
                        foreach(var documentType in documentTypes)
                        {
                            if(documentType.Value != null)
                            {
                                message.Body += documentType.Key + ": " + documentType.Value + "\r\n";
                            }
                        }
                    }
                    else if (extensions.Any(x => entry.FullName.EndsWith(x, StringComparison.OrdinalIgnoreCase))) //is an image file
                    {
                        Log.Information($"Found attachment {entry.FullName}");
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