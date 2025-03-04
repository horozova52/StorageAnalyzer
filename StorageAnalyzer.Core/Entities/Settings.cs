namespace StorageAnalyzer.Core.Entities
{
    public class Settings
    {
        public Guid Id { get; set; }
        public bool EnableAutoCleanup { get; set; }
        public bool EnableDuplicateDetection { get; set; }
        public bool EnableBackup { get; set; }
        public ApplicationUser User { get; set; }
    }
}
