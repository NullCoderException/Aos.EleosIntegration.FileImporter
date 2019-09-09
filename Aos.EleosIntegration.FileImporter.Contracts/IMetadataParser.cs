using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aos.EleosIntegration.FileImporter.Contracts
{
    public interface IMetadataParser
    {
        DriveAxleDocument ParseMetadataFile(string xml);
    }
}
