using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Shared.DataTransferObjects
{
   public class SettingsDto
    {
        public int Id { get; set; }
        public bool EnableAutoCleanup { get; set; }
        public bool EnableDuplicateDetection { get; set; }
        public bool EnableBackup { get; set; }
        public string UserId { get; set; }
    }
}
