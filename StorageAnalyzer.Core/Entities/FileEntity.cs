namespace StorageAnalyzer.Core.Entities
{
    public class FileEntity
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public long SizeInBytes { get; set; }
        public string? Hash { get; set; }
        public DateTime DateModified { get; set; }

        public Guid? FolderId { get; set; }
        public FolderEntity? Folder { get; set; }

        public Guid? DuplicateSetId { get; set; }
        public DuplicateSet DuplicateSet { get; set; }
        public string UserId { get; set; }
    }
}
