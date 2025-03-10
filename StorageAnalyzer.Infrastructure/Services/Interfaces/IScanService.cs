using StorageAnalyzer.Shared.DataTransferObjects;
using System.Threading.Tasks;

namespace StorageAnalyzer.Infrastructure.Services.Interfaces
{
    public interface IScanService
    {

        /// <summary>
        /// Scanează fișierele din calea specificată și returnează o listă de DTO-uri.
        /// </summary>
        /// <param name="path">Calea folderului sau a partiției.</param>
        /// <returns>Listă de fișiere (DTO).</returns>
        List<FileEntityDto> Scan(string path);
    }
}
