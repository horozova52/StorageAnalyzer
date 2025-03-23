namespace StorageAnalyzer.Shared.DataTransferObjects
{
    public class DiskStatusDto
    {
        public string Name { get; set; } = string.Empty;
        public long TotalSize { get; set; }
        public long FreeSpace { get; set; }
        public string FileSystem { get; set; } = string.Empty;
    }
}
