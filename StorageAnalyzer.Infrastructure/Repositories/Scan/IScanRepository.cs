
using StorageAnalyzer.Shared.DataTransferObjects;

public interface IScanRepository
{
    Task<Guid> CreateSessionAsync(ScanSessionDto session, List<FileEntityDto> files);
    Task<List<ScanSessionDto>> GetSessionsAsync(string userId);
    Task<List<FileEntityDto>> GetFilesBySessionAsync(Guid sessionId, string userId);
}
