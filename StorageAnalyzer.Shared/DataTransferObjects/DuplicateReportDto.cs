using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Shared.DataTransferObjects
{
    public class DuplicateReportDto
    {
        public string Hash { get; set; } = string.Empty;
        public List<FileEntityDto> Files { get; set; } = new();
    }
}
