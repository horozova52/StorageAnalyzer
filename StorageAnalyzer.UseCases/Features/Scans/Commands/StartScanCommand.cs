using MediatR;
using StorageAnalyzer.Shared.DataTransferObjects;

public class StartScanCommand : IRequest<ScanSessionDto>
{
    public string Path { get; set; } = string.Empty;
    public bool UseAdvanced { get; set; }
}
