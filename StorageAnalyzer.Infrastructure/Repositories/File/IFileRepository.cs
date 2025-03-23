using StorageAnalyzer.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Infrastructure.Repositories.File
{
    public interface IFileRepository
    {
        Task CreateAsync(FileEntityDto fileDto);
        Task<FileEntityDto?> GetByIdAsync(Guid id, string userId);
        Task<List<FileEntityDto>> GetAllAsync(string userId);
        Task UpdateAsync(FileEntityDto fileDto);
        Task DeleteAsync(Guid id, string userId);
    }
}
