using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using StorageAnalyzer.Shared.DataTransferObjects;


namespace StorageAnalyzer.Infrastructure.Services.FileAccessor
{

    public class FileAccessorProxy : IFileAccessor
    {
        private readonly IFileAccessor _inner;
        private readonly IMemoryCache _cache;
        private readonly ILogger<FileAccessorProxy> _log;

        public FileAccessorProxy(IFileAccessor inner,
                                 IMemoryCache cache,
                                 ILogger<FileAccessorProxy> log)
        {
            _inner = inner;
            _cache = cache;
            _log = log;
        }

        public FileEntityDto GetInfo(string path)
        {
            if (_cache.TryGetValue(path, out FileEntityDto dto))
                return dto;

            dto = _inner.GetInfo(path);
            _cache.Set(path, dto, TimeSpan.FromMinutes(5));
            return dto;
        }

        public byte[] ReadBytes(string path)
        {
            var key = $"bytes:{path}";
            if (_cache.TryGetValue(key, out byte[] data))
                return data;

            data = _inner.ReadBytes(path);
            if (data.Length < 1_048_576)               // <1 MB
                _cache.Set(key, data, TimeSpan.FromMinutes(2));
            return data;
        }

        public Stream OpenRead(string path) => _inner.OpenRead(path);
    }


}
