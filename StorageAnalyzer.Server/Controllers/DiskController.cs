using Microsoft.AspNetCore.Mvc;
using System.IO;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiskController : ControllerBase
    {
        [HttpGet("structure")]
        public ActionResult<DiskDto> GetStructure([FromQuery] string path)
        {
            // 1. Validăm că user-ul a trimis ceva
            if (string.IsNullOrWhiteSpace(path))
            {
                return BadRequest("Path parameter is required, e.g. /api/disk/structure?path=C:\\SomeFolder");
            }

            // 2. Verificăm dacă folderul există
            if (!Directory.Exists(path))
            {
                return NotFound($"Directory not found: {path}");
            }

            // 3. Construim structura arborelui
            var diskNode = BuildDiskTree(path);
            return Ok(diskNode);
        }

        private DiskDto BuildDiskTree(string path)
        {
            var dirInfo = new DirectoryInfo(path);

            // Creăm nodul "folder"
            var rootDto = new DiskDto
            {
                Name = dirInfo.Name,
                FullPath = dirInfo.FullName,
                IsFolder = true,
                LastModified = dirInfo.LastWriteTime
            };

            // 1. Obținem subfolderele
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

            // Dacă am reușit să luăm subfoldere
            if (subDirs != null)
            {
                foreach (var subDir in subDirs)
                {
                    // Apel recursiv
                    var subFolderDto = BuildDiskTree(subDir.FullName);
                    rootDto.Children.Add(subFolderDto);
                }
            }

            // 2. Obținem fișierele
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
                // Alte erori de IO
            }
            catch (Exception ex)
            {
                // Erori generale
            }

            // Dacă am reușit să luăm fișierele
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
