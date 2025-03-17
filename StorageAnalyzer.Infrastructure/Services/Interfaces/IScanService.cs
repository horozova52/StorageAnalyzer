using StorageAnalyzer.Shared.DataTransferObjects;
using System.Threading.Tasks;

namespace StorageAnalyzer.Infrastructure.Services.Interfaces
{
    public interface IScanService
    {
        List<FileEntityDto> Scan(string path);
    }
}
