using StorageAnalyzer.Infrastructure.Services.BackUp.LocalBackUpService;
using StorageAnalyzer.Infrastructure.Services.Interfaces;
using StorageAnalyzer.Infrastructure.Services.Scan;

namespace StorageAnalyzer.Infrastructure.Services.Factories
{
    public class DefaultServiceFactory : IServiceFactory
    {
        public IScanService CreateScanService() => new DefaultScanService();
        public IAnalysisService CreateAnalysisService() => new DuplicateAnalysisService();
        public IBackUpService CreateBackupService() => new LocalBackUpService();
    }
}
