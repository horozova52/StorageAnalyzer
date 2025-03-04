using Microsoft.AspNetCore.Identity;
using StorageAnalyzer.Core.Entities;

namespace StorageAnalyzer.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public ICollection<FolderEntity> Folders { get; set; }
    public ICollection<ScanSession> ScanSessions { get; set; }
    public ICollection<BackupLog> BackupLogs { get; set; }
    public Settings? Settings { get; set; }
}