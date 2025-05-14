using MediatR;
using StorageAnalyzer.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.UseCases.Features.Scans.Queries
{
    public class GetScanSessionsQuery : IRequest<List<ScanSessionDto>> { }
}
