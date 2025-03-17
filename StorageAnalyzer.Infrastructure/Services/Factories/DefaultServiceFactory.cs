using StorageAnalyzer.Infrastructure.Services.BackUp;
using StorageAnalyzer.Infrastructure.Services.Interfaces;

namespace StorageAnalyzer.Infrastructure.Services.Factories
{
    public class DefaultServiceFactory : IServiceFactory
    {
        public IScanService CreateScanService() => new DefaultScanService();
        public IAnalysisService CreateAnalysisService() => new DuplicateAnalysisService();
        public IBackupService CreateBackupService() => new LocalBackUpService();
    }
}
