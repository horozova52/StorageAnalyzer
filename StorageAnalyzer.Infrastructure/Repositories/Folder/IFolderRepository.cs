using StorageAnalyzer.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Infrastructure.Repositories.Folder
{
    public interface IFolderRepository
    {
        Task<Guid> CreateAsync(FolderEntityDto folderDto);
        Task UpdateAsync(FolderEntityDto folderDto);
        Task DeleteAsync(Guid folderId, string userId);
        Task<FolderEntityDto?> GetByIdAsync(Guid folderId, string userId);
        Task<List<FolderEntityDto>> GetAllAsync(string userId);
    } 
    }
