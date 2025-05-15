// Infrastructure/Services/Scan/CachedScanService.cs
using StorageAnalyzer.Infrastructure.Cash;
using StorageAnalyzer.Infrastructure.Services.Interfaces;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Infrastructure.Services.Scan;

public class CachedScanService : IScanService
{
    private readonly IScanService _inner;
    private readonly ScanCacheService _cache;

    public CachedScanService(IScanService inner, ScanCacheService cache)
    {
        _inner = inner;
        _cache = cache;
    }

    public List<FileEntityDto> Scan(string path)
    {
        if (_cache.TryGet(path, out var files)) return files;
        files = _inner.Scan(path);
        _cache.Set(path, files);
        return files;
    }
}
