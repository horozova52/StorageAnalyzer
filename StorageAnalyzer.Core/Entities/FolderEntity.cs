namespace StorageAnalyzer.Core.Entities
{
    public class FolderEntity
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<FileEntity> Files { get; set; }
    }

}
