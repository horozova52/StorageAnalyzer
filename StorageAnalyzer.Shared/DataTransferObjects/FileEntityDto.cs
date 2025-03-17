using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Shared.DataTransferObjects
{
    public class FileEntityDto
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public long SizeInBytes { get; set; }
        public string? Hash { get; set; }
        public DateTime DateModified { get; set; }
        public int? FolderId { get; set; }
        public int? DuplicateSetId { get; set; }
    }
}
