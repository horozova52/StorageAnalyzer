using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Shared.DataTransferObjects
{
    public class ScanLogDto
    {
        public int Id { get; set; }
        public int FolderId { get; set; }
        public int TotalFiles { get; set; }
        public long TotalSize { get; set; }
        public int ScanSessionId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
