using StorageAnalyzer.Infrastructure.Services.Interfaces;
using StorageAnalyzer.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

public class DuplicateAnalysisService : IAnalysisService
{
    public string Analyze(List<FileEntityDto> files)
    {
        var duplicates = files
            .Where(f => !string.IsNullOrEmpty(f.Hash))
            .GroupBy(f => f.Hash)
            .Where(g => g.Count() > 1)
            .Select(g => $"{g.Key} → {g.Count()} files")
            .ToList();

        return duplicates.Count == 0
            ? "[DuplicateAnalysisService] No duplicates found"
            : "[DuplicateAnalysisService]\n" + string.Join('\n', duplicates);
    }
}
