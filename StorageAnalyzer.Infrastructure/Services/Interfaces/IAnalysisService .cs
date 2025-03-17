using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Infrastructure.Services.Interfaces
{
    public interface IAnalysisService
    {
        string Analyze(List<FileEntityDto> files);
    }
}
