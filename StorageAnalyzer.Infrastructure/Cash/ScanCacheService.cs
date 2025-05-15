
using Microsoft.Extensions.Logging;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Infrastructure.Cash;

public sealed class ScanCacheService
{
    private readonly TimeSpan _ttl = TimeSpan.FromMinutes(10);
    private readonly object _gate = new();
    private readonly Dictionary<string, CacheEntry> _cache =
        new(StringComparer.OrdinalIgnoreCase);

    private readonly ILogger<ScanCacheService> _log;
    public ScanCacheService(ILogger<ScanCacheService> log) => _log = log;

    private sealed class CacheEntry
    {
        public DateTime ScannedAt { get; init; }
        public List<FileEntityDto> Files { get; init; } = new();
    }

    public bool TryGet(string path, out List<FileEntityDto> files)
    {
        lock (_gate)
        {
            var hit = _cache.TryGetValue(path, out var entry) &&
                      DateTime.UtcNow - entry!.ScannedAt < _ttl;

            _log.LogInformation("CACHE-{State} {Path}", hit ? "HIT" : "MISS", path);

            if (hit)
            {
                files = entry!.Files;
                return true;
            }
        }

        files = null!;
        return false;
    }

    public void Set(string path, List<FileEntityDto> files)
    {
        lock (_gate)
        {
            _cache[path] = new CacheEntry
            {
                ScannedAt = DateTime.UtcNow,
                Files = files
            };
        }
    }
}
