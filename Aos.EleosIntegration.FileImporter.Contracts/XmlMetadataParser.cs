//  COPYRIGHT (C) 2019,  AOS, LLC.  All rights reserved.
//  Duplication in any media is strictly prohibited without
//  explicit written permission from AOS, LLC.
//
//  Author: Chris Thomas <cthomas@aos.biz>
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Aos.EleosIntegration.FileImporter.Contracts
{
    public class XmlMetadataParser : IMetadataParser
    {
        public DriveAxleDocument ParseMetadataFile(string xml)
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(DriveAxleDocument));

            using (StringReader sr = new StringReader(xml))
            {
                return (DriveAxleDocument)ser.Deserialize(sr);
            }
        }

        public Dictionary<string, string> GetCustomProperties(string xml)
        {
            XDocument doc = XDocument.Parse(xml);
            var xElement = doc.Descendants("CustomProperties");
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (xElement != null)
            {
                foreach (var child in xElement.Elements())
                {
                    result.Add(child.Name.LocalName, child.Value);
                }
            }
            return result;
        }

        public Dictionary<string, string> GetDocumentTypes(string xml)
        {
            XDocument doc = XDocument.Parse(xml);
            var xElement = doc.Descendants("DocumentTypes");
            Dictionary<string, string> result = new Dictionary<string, string>();
            if(xElement != null)
            {
                foreach(var child in xElement.Elements())
                {
                    result.Add(child.Name.LocalName, child.Value);
                }
            }
            return result;
        }
    }
}