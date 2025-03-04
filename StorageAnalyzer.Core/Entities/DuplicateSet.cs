using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Core.Entities
{
    public class DuplicateSet
    {
        public Guid Id { get; set; }
        public string Hash { get; set; }
        public ICollection<FileEntity> Files { get; set; }
    }

}
