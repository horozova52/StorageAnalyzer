using Microsoft.AspNetCore.Mvc;
using StorageAnalyzer.Infrastructure.Services.Factories;
using StorageAnalyzer.Infrastructure.Services.Interfaces;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScanController : ControllerBase
    {
        private readonly DefaultServiceFactory _defaultFactory;
        private readonly AdvancedServiceFactory _advancedFactory;

        // Injectăm ambele fabrici, să putem alege la runtime.
        public ScanController(DefaultServiceFactory defaultFactory, AdvancedServiceFactory advancedFactory)
        {
            _defaultFactory = defaultFactory;
            _advancedFactory = advancedFactory;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestFactory([FromQuery] string path = "C:\\Temp", [FromQuery] bool useAdvanced = false)
        {
            // 1. Alegem fabrica la runtime
            IServiceFactory factory = useAdvanced
                ? (IServiceFactory)_advancedFactory
                : (IServiceFactory)_defaultFactory;

            // 2. Creăm servicii
            var scanService = factory.CreateScanService();
            var analysisService = factory.CreateAnalysisService();
            var backupService = factory.CreateBackupService();

            // 3. Scanăm
            var files = scanService.Scan(path); // Sincron

            // 4. Analizăm
            var analysisResult = analysisService.Analyze(files);

            // 5. Backup asincron (dacă e local sau cloud)
            var backupResult = await backupService.BackupAsync(files, "C:\\Backups\\TestFactory");

            // 6. Returnăm un JSON simplu cu date
            return Ok(new
            {
                UsedFactory = useAdvanced ? "Advanced" : "Default",
                Path = path,
                ScannedFilesCount = files.Count,
                AnalysisResult = analysisResult,
                BackupResult = backupResult,
                FirstFile = files.FirstOrDefault()?.FilePath // doar ca exemplu
            });
        }
    }
}
