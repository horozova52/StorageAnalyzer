using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Infrastructure.Services.Interfaces
{
    public interface IBackUpService
    {
        /// <summary>
        /// Face backup la fișierele specificate, în destinația dată.
        /// </summary>
        /// <param name="files">Fișierele de copiat.</param>
        /// <param name="destination">Folder local sau alt tip de destinație (cloud etc.).</param>
        /// <returns>Mesajul despre ce s-a întâmplat în backup.</returns>
        Task<string> BackupAsync(List<FileEntityDto> files, string destination);
    }
}
