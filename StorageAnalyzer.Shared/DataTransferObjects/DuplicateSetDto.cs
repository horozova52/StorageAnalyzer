using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Shared.DataTransferObjects
{
   public  class DuplicateSetDto
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public List<int> FileIds { get; set; }
    }
}
