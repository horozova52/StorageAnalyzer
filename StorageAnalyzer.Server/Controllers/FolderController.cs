using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageAnalyzer.Usecases.Features.Folders.Commands;
using StorageAnalyzer.Usecases.Features.Folders.Queries;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FolderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FolderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<FolderEntityDto>> CreateFolder([FromBody] CreateFolderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult<bool>> UpdateFolder([FromBody] UpdateFolderCommand command)
        {
            var success = await _mediator.Send(command);
            return success ? Ok(true) : BadRequest(false);
        }

        [HttpDelete("delete/{folderId}")]
        public async Task<ActionResult<bool>> DeleteFolder(Guid folderId)
        {
            var cmd = new DeleteFolderCommand { FolderId = folderId };
            var success = await _mediator.Send(cmd);
            return success ? Ok(true) : NotFound(false);
        }

        [HttpGet("{folderId}")]
        public async Task<ActionResult<FolderEntityDto>> GetFolderById(Guid folderId)
        {
            var query = new GetFolderByIdQuery { FolderId = folderId };
            var folder = await _mediator.Send(query);
            if (folder == null) return NotFound();
            return Ok(folder);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<FolderEntityDto>>> GetAllFolders()
        {
            var query = new GetAllFoldersQuery();
            var folders = await _mediator.Send(query);
            return Ok(folders);
        }
    }
}
