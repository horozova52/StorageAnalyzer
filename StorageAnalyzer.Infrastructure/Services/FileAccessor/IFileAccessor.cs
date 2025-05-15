using StorageAnalyzer.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Infrastructure.Services.FileAccessor
{
    public interface IFileAccessor
    {
        FileEntityDto GetInfo(string path);          // meta-date
        byte[] ReadBytes(string path);        // conținut (opțional)
        Stream OpenRead(string path);         // stream, dacă avem nevoie
    }
}
