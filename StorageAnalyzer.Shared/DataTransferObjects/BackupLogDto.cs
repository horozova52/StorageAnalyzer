﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Shared.DataTransferObjects
{
    public class BackupLogDto
    {
        public Guid Id { get; set; }
        public DateTime BackupDate { get; set; }
        public string BackupPath { get; set; }
        public long TotalSize { get; set; }
        public string UserId { get; set; }
    }
}
