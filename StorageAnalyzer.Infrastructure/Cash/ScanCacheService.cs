using StorageAnalyzer.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Infrastructure.Cash
{
    public sealed class ScanCacheService
    {
        private readonly TimeSpan _ttl = TimeSpan.FromMinutes(10);  // cât timp e “valabilă” o scanare
        private readonly object _gate = new();                     // lock simplu
        private readonly Dictionary<string, CacheEntry> _cache = new(StringComparer.OrdinalIgnoreCase);

        private class CacheEntry
        {
            public DateTime ScannedAt { get; init; }
            public List<FileEntityDto> Files { get; init; } = new();
        }

        /// Tentează să returneze lista de fişiere din cache;
        /// dacă nu există sau a expirat -> returnează null.
        public bool TryGet(string path, out List<FileEntityDto> files)
        {
            lock (_gate)
            {
                if (_cache.TryGetValue(path, out var entry) &&
                    DateTime.UtcNow - entry.ScannedAt < _ttl)
                {
                    files = entry.Files;
                    return true;
                }
            }
            files = null!;
            return false;
        }

        /// Înregistrează rezultatul unei scanări.
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

}
