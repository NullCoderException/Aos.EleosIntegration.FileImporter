using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
