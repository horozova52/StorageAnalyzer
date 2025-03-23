using Microsoft.EntityFrameworkCore;
using StorageAnalyzer.Core.Entities;
using StorageAnalyzer.Infrastructure.Contexts;
using StorageAnalyzer.Infrastructure.Repositories.Folder;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Infrastructure.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FolderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateAsync(FolderEntityDto folderDto)
        {
            var folder = new FolderEntity
            {
                Path = folderDto.Path,
                Name = folderDto.Name,
                UserId = folderDto.UserId
            };
            _dbContext.Folders.Add(folder);
            await _dbContext.SaveChangesAsync();
            return folder.Id;
        }

        public async Task UpdateAsync(FolderEntityDto folderDto)
        {
            var folder = await _dbContext.Folders
                .FirstOrDefaultAsync(f => f.Id == folderDto.Id && f.UserId == folderDto.UserId);

            if (folder != null)
            {
                folder.Path = folderDto.Path;
                folder.Name = folderDto.Name;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid folderId, string userId)
        {
            var folder = await _dbContext.Folders
                .FirstOrDefaultAsync(f => f.Id == folderId && f.UserId == userId);

            if (folder != null)
            {
                _dbContext.Folders.Remove(folder);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<FolderEntityDto?> GetByIdAsync(Guid folderId, string userId)
        {
            var folder = await _dbContext.Folders
                .FirstOrDefaultAsync(f => f.Id == folderId && f.UserId == userId);

            if (folder == null) return null;

            return new FolderEntityDto
            {
                Id = folder.Id,
                Path = folder.Path,
                Name = folder.Name,
                UserId = folder.UserId
            };
        }

        public async Task<List<FolderEntityDto>> GetAllAsync(string userId)
        {
            return await _dbContext.Folders
                .Where(f => f.UserId == userId)
                .Select(f => new FolderEntityDto
                {
                    Id = f.Id,
                    Path = f.Path,
                    Name = f.Name,
                    UserId = f.UserId
                })
                .ToListAsync();
        }
    }
}
