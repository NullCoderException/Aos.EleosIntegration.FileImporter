using Aos.EleosIntegration.FileImporter.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aos.EleosIntegration.FileImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            TestFileSweeper();
        }

        static void TestFileSweeper()
        {
            const string dir = @"C:\Eleos\";
            IFileSweeper sweeper = new ZipFileSweeper();
            

            var zips = sweeper.FindZipFilesInDirectory(dir);
            foreach(var zip in zips)
            {
                Console.WriteLine(zip);
                sweeper.ProcessZipFile(zip);
            }
            Console.ReadLine();
            
        }


    }
}
