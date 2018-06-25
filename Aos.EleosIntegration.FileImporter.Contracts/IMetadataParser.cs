using Aos.EleosIntegration.FileImporter.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aos.EleosIntegration.FileImporter.Contracts
{
    public interface IMetadataParser
    {
        FileMetadata ParseMetadataFile(string fileName);
    }
}
