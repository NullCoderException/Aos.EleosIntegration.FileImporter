using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aos.EleosIntegration.FileImporter.Dto
{
    public class FileMetadata
    {
        public string DriverAxleId { get; set; }
        public string SDKUserId { get; set; }
        public string CreatedBy { get; set; }
        public int NumberOfPages { get; set; }
        //Identifiers
        public string ConfirmationNumber { get; set; }
        public string LoadNumber { get; set; }
        public string BillOfLadingNumber { get; set; }

        public DateTime CreatedAt { get; set; }
        public string DownloadUrl { get; set; }

        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime SentAt { get; set; }
        public bool Release { get; set; }
        public bool Indemnify { get; set; }

        public string DocumentType { get; set; }
    }
}
