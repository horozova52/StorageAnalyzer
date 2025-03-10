namespace StorageAnalyzer.Infrastructure.Services.Interfaces
{
    public interface IServiceFactory
    {
        IScanService CreateScanService();
        IAnalysisService CreateAnalysisService();
        IBackUpService CreateBackupService();
    }
}
