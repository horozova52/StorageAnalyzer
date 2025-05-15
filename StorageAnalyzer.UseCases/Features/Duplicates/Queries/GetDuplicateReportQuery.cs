
using MediatR;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.UseCases.Features.Duplicates.Queries;
public class GetDuplicateReportQuery : IRequest<List<DuplicateReportDto>>
{
    public Guid ScanSessionId { get; set; }
}
