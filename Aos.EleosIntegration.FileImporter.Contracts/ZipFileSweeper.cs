using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aos.EleosIntegration.FileImporter.Contracts
{
    public class ZipFileSweeper : IFileSweeper
    {
        public List<string> FindZipFilesInDirectory(string dir)
        {
            var ext = new List<string> { ".zip" };
            var myFiles = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories)
                 .Where(s => ext.Contains(Path.GetExtension(s)));
            return myFiles.ToList();
        }

        public void ProcessZipFile(string zipPath)
        {
            // Normalizes the path.
            zipPath = Path.GetFullPath(zipPath);
            //string unzipFolder = zipPath.Replace(".zip", "");
           // ZipFile.ExtractToDirectory(zipPath, unzipFolder);

            
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {                  
                    Console.WriteLine(entry.FullName.PadLeft(5));
                    if (entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {                    
                        StreamReader reader = new StreamReader(entry.Open());
                        IMetadataParser parser = new XmlMetadataParser();
                        var xmlData = parser.ParseMetadataFile(reader.ReadToEnd());
                    }
                }
            }
        }
    }
}
