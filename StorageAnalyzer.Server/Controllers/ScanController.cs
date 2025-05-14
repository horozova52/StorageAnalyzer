using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageAnalyzer.Infrastructure.Services.Factories;
using StorageAnalyzer.Infrastructure.Services.Interfaces;
using StorageAnalyzer.Shared.DataTransferObjects;
using StorageAnalyzer.UseCases.Features.Scans.Queries;

namespace StorageAnalyzer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScanController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IServiceFactory _factory;   // una singură (default SAU advanced)

        public ScanController(IMediator mediator, IServiceFactory factory)
        {
            _mediator = mediator;
            _factory = factory;      // DI îţi dă implementarea înregistrată în Program.cs
        }

        // ---------------- CQRS endpoints ----------------
        [HttpPost("start")]
        public async Task<ActionResult<ScanSessionDto>> StartScan([FromBody] StartScanCommand cmd)
            => Ok(await _mediator.Send(cmd));

        [HttpGet("sessions")]
        public async Task<ActionResult<List<ScanSessionDto>>> Sessions()
            => Ok(await _mediator.Send(new GetScanSessionsQuery()));

        [HttpGet("files/{sessionId}")]
        public async Task<ActionResult<List<FileEntityDto>>> Files(Guid sessionId)
            => Ok(await _mediator.Send(new GetFilesBySessionQuery { SessionId = sessionId }));

        // ---------------- Abstract-Factory demo ----------------
        [HttpGet("test")]
        public async Task<IActionResult> TestFactory([FromQuery] string path = "C:\\Temp")
        {
            var scanService = _factory.CreateScanService();
            var analysisService = _factory.CreateAnalysisService();
            var backupService = _factory.CreateBackupService();

            var files = scanService.Scan(path);
            var analysisResult = analysisService.Analyze(files);
            var backupResult = await backupService.BackupAsync(files, "C:\\Backups\\TestFactory");

            return Ok(new
            {
                FactoryType = _factory.GetType().Name,
                Path = path,
                ScannedFilesCount = files.Count,
                AnalysisResult = analysisResult,
                BackupResult = backupResult
            });
        }
    }

}
