using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Infrastructure.Services.Interfaces
{
    public interface IBackupService
    {
        Task<string> BackupAsync(List<FileEntityDto> files, string destination);

    }
}
