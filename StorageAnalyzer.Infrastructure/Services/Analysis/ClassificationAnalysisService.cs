using StorageAnalyzer.Infrastructure.Services.Interfaces;
using StorageAnalyzer.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class ClassificationAnalysisService : IAnalysisService
{
    public string Analyze(List<FileEntityDto> files)
    {
        // Simplu exemplu: contorizez fișiere .docx/.pdf și .jpg/.png
        int docCount = files.Count(f => f.FilePath.EndsWith(".docx") || f.FilePath.EndsWith(".pdf"));
        int imgCount = files.Count(f => f.FilePath.EndsWith(".jpg") || f.FilePath.EndsWith(".png"));

        return $"[ClassificationAnalysisService] Documents: {docCount}, Images: {imgCount}, Total: {files.Count}";
    }
}
