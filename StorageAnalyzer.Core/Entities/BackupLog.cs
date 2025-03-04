namespace StorageAnalyzer.Core.Entities
{
    public class BackupLog
    {
        public Guid Id { get; set; }
        public DateTime BackupDate { get; set; }
        public string BackupPath { get; set; }
        public long TotalSize { get; set; }
        public ApplicationUser User { get; set; }
    }
}
