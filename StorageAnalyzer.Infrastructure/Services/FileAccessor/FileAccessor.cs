using StorageAnalyzer.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Infrastructure.Services.FileAccessor
{
    public class FileAccessor : IFileAccessor
    {
        public FileEntityDto GetInfo(string path)
        {
            var fi = new FileInfo(path);
            return new FileEntityDto
            {
                FilePath = fi.FullName,
                SizeInBytes = fi.Length,
                DateModified = fi.LastWriteTime
            };
        }

        public byte[] ReadBytes(string path) => File.ReadAllBytes(path);
        public Stream OpenRead(string path) => File.OpenRead(path);
    }
}
