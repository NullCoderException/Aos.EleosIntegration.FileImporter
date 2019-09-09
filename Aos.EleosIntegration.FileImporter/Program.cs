//  COPYRIGHT (C) 2019,  AOS, LLC.  All rights reserved.
//  Duplication in any media is strictly prohibited without
//  explicit written permission from AOS, LLC.
//
//  Author: Chris Thomas <cthomas@aos.biz>
using Aos.EleosIntegration.FileImporter.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aos.EleosIntegration.FileImporter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TestFileSweeper();
        }

        private static void TestFileSweeper()
        {
            const string dir = @"C:\Eleos\";
            IEleosFileImporter sweeper = new EleosZipFileImporter();

            var zips = sweeper.FindZipFilesInDirectory(dir);
            foreach (var zip in zips)
            {
                Console.WriteLine(zip);
                sweeper.ProcessZipFile(zip);
            }
        }
    }
}