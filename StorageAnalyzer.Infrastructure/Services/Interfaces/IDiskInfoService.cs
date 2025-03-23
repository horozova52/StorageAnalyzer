using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Infrastructure.Services.WMI
{
    public interface IDiskInfoService
    {
        Task<List<DiskStatusDto>> GetDiskInfoAsync();
    }
}
