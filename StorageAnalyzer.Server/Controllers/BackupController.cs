// StorageAnalyzer.Server.Controllers/BackupController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageAnalyzer.Shared.DataTransferObjects;
using StorageAnalyzer.Usecases.Features.Backups.Commands;
using StorageAnalyzer.UseCases.Features.Backups.Commands;
using StorageAnalyzer.UseCases.Features.Backups.Queries;

namespace StorageAnalyzer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BackupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BackupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<BackupLogDto>> CreateBackup([FromBody] CreateBackupCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("delete/{backupId}")]
        public async Task<ActionResult<bool>> DeleteBackup(Guid backupId)
        {
            var cmd = new DeleteBackupCommand { BackupId = backupId };
            var success = await _mediator.Send(cmd);
            if (!success) return NotFound(false);
            return Ok(true);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<BackupLogDto>>> GetAllBackups()
        {
            var query = new GetAllBackupsQuery();
            var logs = await _mediator.Send(query);
            return Ok(logs);
        }
    }
}
