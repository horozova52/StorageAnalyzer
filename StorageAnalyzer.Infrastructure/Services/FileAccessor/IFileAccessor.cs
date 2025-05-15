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
        FileEntityDto GetInfo(string path);         
        byte[] ReadBytes(string path);        
        Stream OpenRead(string path);       
    }
}
