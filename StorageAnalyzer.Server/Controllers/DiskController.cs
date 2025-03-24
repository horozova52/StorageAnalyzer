using Microsoft.AspNetCore.Mvc;
using System.IO;
using StorageAnalyzer.Shared.DataTransferObjects;
using StorageAnalyzer.Infrastructure.Services.WMI;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiskController : ControllerBase
    {
        private readonly IDiskInfoService _diskInfoService;

        public DiskController(IDiskInfoService diskInfoService)
        {
            _diskInfoService = diskInfoService;
        }

        [HttpGet("status")]
        public async Task<ActionResult<List<DiskStatusDto>>> GetDiskStatus()
        {
            var disks = await _diskInfoService.GetDiskInfoAsync();
            return Ok(disks);
        }

        [HttpGet("structure")]
        public ActionResult<DiskDto> GetStructure([FromQuery] string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return BadRequest("Path parameter is required, e.g. /api/disk/structure?path=C:\\SomeFolder");
            }

            if (!Directory.Exists(path))
            {
                return NotFound($"Directory not found: {path}");
            }

            var diskNode = BuildDiskTree(path);
            return Ok(diskNode);
        }

        [HttpGet("scan")]
        public ActionResult<List<DiskDto>> ScanFiles([FromQuery] string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return BadRequest("Path parameter is required, e.g. /api/disk/scan?path=C:\\SomeFolder");
            }

            if (!Directory.Exists(path))
            {
                return NotFound($"Directory not found: {path}");
            }

            var files = GetFiles(path);
            return Ok(files);
        }

        private List<DiskDto> GetFiles(string path)
        {
            var fileDtos = new List<DiskDto>();
            var dirInfo = new DirectoryInfo(path);

            FileInfo[] files = null;
            try
            {
                files = dirInfo.GetFiles();
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (IOException ex)
            {
            }
            catch (Exception ex)
            {
            }

            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileDto = new DiskDto
                    {
                        Name = file.Name,
                        FullPath = file.FullName,
                        Size = file.Length,
                        LastModified = file.LastWriteTime
                    };
                    fileDtos.Add(fileDto);
                }
            }

            return fileDtos;
        }

        private DiskDto BuildDiskTree(string path)
        {
            var dirInfo = new DirectoryInfo(path);

            var rootDto = new DiskDto
            {
                Name = dirInfo.Name,
                FullPath = dirInfo.FullName,
                IsFolder = true,
                LastModified = dirInfo.LastWriteTime
            };

            DirectoryInfo[] subDirs = null;
            try
            {
                subDirs = dirInfo.GetDirectories();
            }
            catch (UnauthorizedAccessException)
            {
                rootDto.Children.Add(new DiskDto
                {
                    Name = "[ACCESS DENIED]",
                    FullPath = path,
                    IsFolder = true
                });
            }
            catch (IOException ex)
            {
            }
            catch (Exception ex)
            {
            }

            if (subDirs != null)
            {
                foreach (var subDir in subDirs)
                {
                    var subFolderDto = BuildDiskTree(subDir.FullName);
                    rootDto.Children.Add(subFolderDto);
                }
            }

            FileInfo[] files = null;
            try
            {
                files = dirInfo.GetFiles();
            }
            catch (UnauthorizedAccessException)
            {
                rootDto.Children.Add(new DiskDto
                {
                    Name = "[ACCESS DENIED]",
                    FullPath = path,
                    IsFolder = true
                });
            }
            catch (IOException ex)
            {
            }
            catch (Exception ex)
            {
            }

            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileDto = new DiskDto
                    {
                        Name = file.Name,
                        FullPath = file.FullName,
                        IsFolder = false,
                        Size = file.Length,
                        LastModified = file.LastWriteTime
                    };
                    rootDto.Children.Add(fileDto);
                }
            }

            return rootDto;
        }
    }
}
