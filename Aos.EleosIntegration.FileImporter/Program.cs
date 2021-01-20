//  COPYRIGHT (C) 2019,  AOS, LLC.  All rights reserved.
//  Duplication in any media is strictly prohibited without
//  explicit written permission from AOS, LLC.
//
//  Author: Chris Thomas <cthomas@aos.biz>
using Aos.EleosIntegration.FileImporter.Contracts;
using Serilog;
using System;
using System.Configuration;

namespace Aos.EleosIntegration.FileImporter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("Logs\\myapp.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            ProcessZipFiles();

            Log.CloseAndFlush();
        }

        private static void ProcessZipFiles()
        {
            //string dir = @"C:\Eleos\";
            string dir = ConfigurationManager.AppSettings["DataDirectory"];
            IEleosFileImporter sweeper = new EleosZipFileImporter();

            var zips = sweeper.FindZipFilesInDirectory(dir);
            Log.Information("Starting file emailing process");
            foreach (var zip in zips)
            {
                Console.WriteLine(zip);
                sweeper.ProcessZipFile(zip);
            }
            Log.Information("....File emailing process complete");
        }
    }
}