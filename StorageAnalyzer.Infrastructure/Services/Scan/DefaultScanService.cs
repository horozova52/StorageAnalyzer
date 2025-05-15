
using StorageAnalyzer.Infrastructure.Services.Interfaces;
using StorageAnalyzer.Core.Entities;
using StorageAnalyzer.Infrastructure.Contexts;
using System;
using System.IO;
using StorageAnalyzer.Shared.DataTransferObjects;
using StorageAnalyzer.Infrastructure.Services.Hash;

public class DefaultScanService : IScanService
{
    public List<FileEntityDto> Scan(string path)
    {
        var result = new List<FileEntityDto>();

        if (!Directory.Exists(path))
        {
            // Poți fie să arunci excepție, fie să întorci listă goală
            return result;
        }

        var files = Directory.GetFiles(path);
        foreach (var filePath in files)
        {
            var info = new FileInfo(filePath);
            result.Add(new FileEntityDto
            {
                FilePath = info.FullName,
                SizeInBytes = info.Length,
                DateModified = info.LastWriteTime,

                 Hash = HashService.Sha256(filePath)
            });
        }

        return result;
    }
}

