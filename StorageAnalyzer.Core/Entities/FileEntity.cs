﻿namespace StorageAnalyzer.Core.Entities
{
    public class FileEntity
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public long SizeInBytes { get; set; }
        public string Hash { get; set; }
        public DateTime DateModified { get; set; }

        public int FolderId { get; set; }
        public FolderEntity Folder { get; set; }
        public ApplicationUser User { get; set; }

        public int? DuplicateSetId { get; set; }
        public DuplicateSet DuplicateSet { get; set; }
    }
}
