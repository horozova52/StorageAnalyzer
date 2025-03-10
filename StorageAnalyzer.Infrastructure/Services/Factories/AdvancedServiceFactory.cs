using StorageAnalyzer.Infrastructure.Services.Interfaces;
using StorageAnalyzer.Infrastructure.Services.Scan;
using StorageAnalyzer.Infrastructure.Services.BackUp;

namespace StorageAnalyzer.Infrastructure.Services.Factories
{
    public class AdvancedServiceFactory : IServiceFactory
    {
        public IScanService CreateScanService() => new AdvancedScanService();
        public IAnalysisService CreateAnalysisService() => new ClassificationAnalysisService();
        public IBackUpService CreateBackupService() => new CloudBackUpService();
    }
}
