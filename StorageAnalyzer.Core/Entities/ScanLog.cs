using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Core.Entities
{
    public class ScanLog
    {
        public Guid Id { get; set; }
        public Guid FolderId { get; set; }
        public FolderEntity Folder { get; set; }

        public int TotalFiles { get; set; }
        public long TotalSize { get; set; }

        public Guid  ScanSessionId { get; set; }
        public ScanSession ScanSession { get; set; }
    }
}
