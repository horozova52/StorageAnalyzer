using StorageAnalyzer.Infrastructure.Cash;
using StorageAnalyzer.Infrastructure.Services.BackUp;
using StorageAnalyzer.Infrastructure.Services.Interfaces;
using StorageAnalyzer.Infrastructure.Services.Scan;

namespace StorageAnalyzer.Infrastructure.Services.Factories
{
    public class DefaultServiceFactory : IServiceFactory
    {
        private readonly ScanCacheService _cache;

        public DefaultServiceFactory(ScanCacheService cache) => _cache = cache;

        public IScanService CreateScanService() => new CachedScanService(new DefaultScanService(), _cache);
        public IAnalysisService CreateAnalysisService() => new DuplicateAnalysisService();
        public IBackupService CreateBackupService() => new LocalBackUpService();
    }

}
