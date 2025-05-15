
using MediatR;
using StorageAnalyzer.Infrastructure.Repositories.File;
using StorageAnalyzer.Shared.DataTransferObjects;
using StorageAnalyzer.UseCases.Features.Duplicates.Queries;

namespace StorageAnalyzer.UseCases.Features.Duplicates.Handlers;

public class DuplicateHandler :
    IRequestHandler<GetDuplicateReportQuery, List<DuplicateReportDto>>
{
    private readonly IFileRepository _files;
    public DuplicateHandler(IFileRepository files) => _files = files;

    public async Task<List<DuplicateReportDto>> Handle(
        GetDuplicateReportQuery request, CancellationToken ct)
    {
        var files = await _files.GetBySessionAsync(request.ScanSessionId); // existing repo
        return files.Where(f => !string.IsNullOrEmpty(f.Hash))
                    .GroupBy(f => f.Hash)
                    .Where(g => g.Count() > 1)
                    .Select(g => new DuplicateReportDto
                    {
                        Hash = g.Key!,
                        Files = g.ToList()
                    })
                    .ToList();
    }
}
