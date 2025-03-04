using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Shared.DataTransferObjects
{
    public class ScanSessionDto
    {
        public int Id { get; set; }
        public DateTime ScanDate { get; set; }
        public DateTime? EndTime { get; set; }
        public string UserId { get; set; }
    }
}
