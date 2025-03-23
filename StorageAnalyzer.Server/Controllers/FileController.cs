using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageAnalyzer.UseCases.Features.Files.Commands;
using StorageAnalyzer.UseCases.Features.Files.Queries;
using StorageAnalyzer.Shared.DataTransferObjects;
using StorageAnalyzer.UseCases.Files.Commands;

namespace StorageAnalyzer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<FileEntityDto>> CreateFile([FromBody] CreateFileCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult<bool>> UpdateFile([FromBody] UpdateFileCommand command)
        {
            var success = await _mediator.Send(command);
            return success ? Ok(true) : NotFound(false);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult<bool>> DeleteFile(Guid id)
        {
            var cmd = new DeleteFileCommand { FileId = id };
            var success = await _mediator.Send(cmd);
            return success ? Ok(true) : NotFound(false);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FileEntityDto?>> GetFileById(Guid id)
        {
            var query = new GetFileByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<FileEntityDto>>> GetAllFiles()
        {
            var query = new GetAllFileQuery();
            var list = await _mediator.Send(query);
            return Ok(list);
        }
    }
}
