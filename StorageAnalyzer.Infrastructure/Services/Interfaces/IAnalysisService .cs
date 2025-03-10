using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Infrastructure.Services.Interfaces
{
    public interface IAnalysisService
    {
        /// <summary>
        /// Analizează fișierele (ex. detectare duplicate, clasificare etc.)
        /// </summary>
        /// <param name="files">Fișierele de analizat.</param>
        /// <returns>Un raport text cu rezultatul analizei.</returns>
        string Analyze(List<FileEntityDto> files);
    }
}
