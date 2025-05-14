namespace StorageAnalyzer.Core.Entities
{
    public class ScanSession
    {
        public Guid Id { get; set; }
        public DateTime ScanDate { get; set; }
        public DateTime? EndTime { get; set; }
        public ICollection<ScanLog> ScanLogs { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TotalFiles { get; set; }
        public long TotalSize { get; set; }
    }

}
