using StorageAnalyzer.Infrastructure.Cash;
using StorageAnalyzer.Infrastructure.Services.Interfaces;
using StorageAnalyzer.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Infrastructure.Services.Scan
{
    public class CachedScanService : IScanService
    {
        private readonly IScanService _inner;    // DefaultScanService sau AdvancedScanService
        private readonly ScanCacheService _cache;

        public CachedScanService(IScanService inner, ScanCacheService cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public List<FileEntityDto> Scan(string path)
        {
            if (_cache.TryGet(path, out var files))
                return files;                             // hit

            var fresh = _inner.Scan(path);               // miss
            _cache.Set(path, fresh);
            return fresh;
        }
    }
}
