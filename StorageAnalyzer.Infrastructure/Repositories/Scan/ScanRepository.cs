using Microsoft.EntityFrameworkCore;
using StorageAnalyzer.Core.Entities;
using StorageAnalyzer.Infrastructure.Contexts;
using StorageAnalyzer.Shared.DataTransferObjects;

public class ScanRepository : IScanRepository
{
    private readonly ApplicationDbContext _db;

    public ScanRepository(ApplicationDbContext db) => _db = db;

    public async Task<Guid> CreateSessionAsync(ScanSessionDto dto, List<FileEntityDto> files)
    {
        var session = new ScanSession      // entitate EF
        {
            Id = Guid.NewGuid(),
            ScanDate = dto.ScanDate,
            EndTime = dto.EndTime,
            TotalFiles = dto.TotalFiles,
            TotalSize = dto.TotalSize,
            UserId = dto.UserId
        };
        _db.ScanSessions.Add(session);

        // mapăm fişierele
        foreach (var f in files)
        {
            _db.Files.Add(new FileEntity
            {
                Id = Guid.NewGuid(),
                FilePath = f.FilePath,
                SizeInBytes = f.SizeInBytes,
                DateModified = f.DateModified,
                ScanSessionId = session.Id,
                UserId = dto.UserId
            });
        }
        await _db.SaveChangesAsync();
        return session.Id;
    }

    public async Task<List<ScanSessionDto>> GetSessionsAsync(string userId)
        => await _db.ScanSessions
            .Where(s => s.UserId == userId)
            .Select(s => new ScanSessionDto
            {
                Id = s.Id,
                EndTime = s.EndTime,
                ScanDate = s.ScanDate,
                TotalFiles = s.TotalFiles,
                TotalSize = s.TotalSize,
                UserId = s.UserId
            }).ToListAsync();

    public async Task<List<FileEntityDto>> GetFilesBySessionAsync(Guid id, string userId)
        => await _db.Files
            .Where(f => f.ScanSessionId == id && f.UserId == userId)
            .Select(f => new FileEntityDto
            {
                Id = f.Id,
                FilePath = f.FilePath,
                SizeInBytes = f.SizeInBytes,
                DateModified = f.DateModified,
                UserId = f.UserId
            }).ToListAsync();
}
