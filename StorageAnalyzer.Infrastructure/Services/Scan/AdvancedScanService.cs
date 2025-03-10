
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using StorageAnalyzer.Infrastructure.Services.Interfaces;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Infrastructure.Services.Scan;
public class AdvancedScanService : IScanService
{
    public List<FileEntityDto> Scan(string path)
    {
        // Aici ai putea adăuga logică de paralelizare, caching, etc.
        // Deocamdată, exemplu simplu care delegă la DefaultScanService.
        var defaultScan = new DefaultScanService();
        return defaultScan.Scan(path);
    }
}
