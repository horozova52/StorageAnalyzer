using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageAnalyzer.Shared.DataTransferObjects;
using StorageAnalyzer.UseCases.Features.Duplicates.Queries;

namespace StorageAnalyzer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DuplicatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DuplicatesController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{sessionId}")]
        public async Task<ActionResult<List<DuplicateReportDto>>> Report(Guid sessionId)
        {
            var result = await _mediator.Send(new GetDuplicateReportQuery
            {
                ScanSessionId = sessionId
            });
            return Ok(result);
        }
    }
}
