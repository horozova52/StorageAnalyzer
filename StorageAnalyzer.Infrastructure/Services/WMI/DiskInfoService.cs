using System.Management;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Infrastructure.Services.WMI
{
    public class DiskInfoService : IDiskInfoService
    {
        public async Task<List<DiskStatusDto>> GetDiskInfoAsync()
        {
            var disks = new List<DiskStatusDto>();

            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DriveType=3");

            foreach (ManagementObject drive in searcher.Get())
            {
                disks.Add(new DiskStatusDto
                {
                    Name = drive["Name"]?.ToString() ?? "",
                    TotalSize = Convert.ToInt64(drive["Size"] ?? 0),
                    FreeSpace = Convert.ToInt64(drive["FreeSpace"] ?? 0),
                    FileSystem = drive["FileSystem"]?.ToString() ?? "Unknown"
                });
            }

            return await Task.FromResult(disks);
        }
    }
}
