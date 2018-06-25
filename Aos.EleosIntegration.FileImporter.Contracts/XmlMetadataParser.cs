using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Aos.EleosIntegration.FileImporter.Dto;

namespace Aos.EleosIntegration.FileImporter.Contracts
{
    public class XmlMetadataParser : IMetadataParser
    {
        public FileMetadata ParseMetadataFile(string fileName)
        {
            XDocument document = XDocument.Load(fileName);
            return null;
        }
    }
}
