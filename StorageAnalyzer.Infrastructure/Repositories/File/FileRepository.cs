using Microsoft.EntityFrameworkCore;
using StorageAnalyzer.Core.Entities;
using StorageAnalyzer.Infrastructure.Contexts;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Infrastructure.Repositories.File
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(FileEntityDto fileDto)
        {
            var fileEntity = new FileEntity
            {
                Id = Guid.NewGuid(),
                FilePath = fileDto.FilePath,
                SizeInBytes = fileDto.SizeInBytes,
                Hash = fileDto.Hash,
                DateModified = fileDto.DateModified,
                UserId = fileDto.UserId
            };
            _dbContext.Files.Add(fileEntity);
            await _dbContext.SaveChangesAsync();
            fileDto.Id = fileEntity.Id;
        }

        public async Task<FileEntityDto?> GetByIdAsync(Guid id, string userId)
        {
            var fileEntity = await _dbContext.Files
                .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);
            if (fileEntity == null) return null;

            return new FileEntityDto
            {
                Id = fileEntity.Id,
                FilePath = fileEntity.FilePath,
                SizeInBytes = fileEntity.SizeInBytes,
                Hash = fileEntity.Hash,
                DateModified = fileEntity.DateModified,
                UserId = fileEntity.UserId
            };
        }

        public async Task<List<FileEntityDto>> GetAllAsync(string userId)
        {
            return await _dbContext.Files
                .Where(f => f.UserId == userId)
                .Select(f => new FileEntityDto
                {
                    Id = f.Id,
                    FilePath = f.FilePath,
                    SizeInBytes = f.SizeInBytes,
                    Hash = f.Hash,
                    DateModified = f.DateModified,
                    UserId = f.UserId
                })
                .ToListAsync();
        }

        public async Task UpdateAsync(FileEntityDto fileDto)
        {
            var fileEntity = await _dbContext.Files
                .FirstOrDefaultAsync(f => f.Id == fileDto.Id && f.UserId == fileDto.UserId);

            if (fileEntity == null) return;

            fileEntity.FilePath = fileDto.FilePath;
            fileEntity.SizeInBytes = fileDto.SizeInBytes;
            fileEntity.Hash = fileDto.Hash;
            fileEntity.DateModified = fileDto.DateModified;

            _dbContext.Files.Update(fileEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, string userId)
        {
            var fileEntity = await _dbContext.Files
                .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);
            if (fileEntity != null)
            {
                _dbContext.Files.Remove(fileEntity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<FileEntityDto>> GetBySessionAsync(Guid sessionId)
        {
            return await _dbContext.Files
                .Where(f => f.ScanSessionId == sessionId)
                .Select(f => new FileEntityDto
                {
                    Id = f.Id,
                    FilePath = f.FilePath,
                    SizeInBytes = f.SizeInBytes,
                    Hash = f.Hash,
                    DateModified = f.DateModified,
                    UserId = f.UserId
                })
                .ToListAsync();
        }
    }
}
