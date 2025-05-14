using MediatR;
using Microsoft.AspNetCore.Http;
using StorageAnalyzer.Infrastructure.Services.Interfaces;
using StorageAnalyzer.Shared.DataTransferObjects;
using StorageAnalyzer.UseCases.Features.Scans.Queries;
using System.Security.Claims;

public class ScanHandler :
    IRequestHandler<StartScanCommand, ScanSessionDto>,
    IRequestHandler<GetScanSessionsQuery, List<ScanSessionDto>>,
    IRequestHandler<GetFilesBySessionQuery, List<FileEntityDto>>
{
    private readonly IServiceFactory _factory;
    private readonly IScanRepository _repo;
    private readonly IHttpContextAccessor _ctx;

    public ScanHandler(IServiceFactory factory, IScanRepository repo, IHttpContextAccessor ctx)
    {
        _factory = factory;
        _repo = repo;
        _ctx = ctx;
    }

    // Start scan
    public async Task<ScanSessionDto> Handle(StartScanCommand cmd, CancellationToken ct)
    {
        var userId = _ctx.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? throw new UnauthorizedAccessException();

        var scanSvc = _factory.CreateScanService();         // Default / Advanced
        var files = scanSvc.Scan(cmd.Path);               // sincr. scanare
        var sessionDto = new ScanSessionDto
        {
            ScanDate = DateTime.UtcNow,
            EndTime = DateTime.UtcNow,
            TotalFiles = files.Count,
            TotalSize = files.Sum(f => f.SizeInBytes),
            UserId = userId
        };

        sessionDto.Id = await _repo.CreateSessionAsync(sessionDto, files);
        return sessionDto;
    }

    // List sessions
    public Task<List<ScanSessionDto>> Handle(GetScanSessionsQuery q, CancellationToken ct)
    {
        var userId = _ctx.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? throw new UnauthorizedAccessException();
        return _repo.GetSessionsAsync(userId);
    }

    // List files of a session
    public Task<List<FileEntityDto>> Handle(GetFilesBySessionQuery q, CancellationToken ct)
    {
        var userId = _ctx.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? throw new UnauthorizedAccessException();
        return _repo.GetFilesBySessionAsync(q.SessionId, userId);
    }
}
