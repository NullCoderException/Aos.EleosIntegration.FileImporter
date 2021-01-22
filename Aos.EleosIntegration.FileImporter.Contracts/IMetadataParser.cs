﻿//  COPYRIGHT (C) 2019,  AOS, LLC.  All rights reserved.
//  Duplication in any media is strictly prohibited without
//  explicit written permission from AOS, LLC.
//
//  Author: Chris Thomas <cthomas@aos.biz>
using System.Collections.Generic;

namespace Aos.EleosIntegration.FileImporter.Contracts
{
    public interface IMetadataParser
    {
        DriveAxleDocument ParseMetadataFile(string xml);

        Dictionary<string, string> GetCustomProperties(string xml);

        Dictionary<string, string> GetDocumentTypes(string xml);
    }
}