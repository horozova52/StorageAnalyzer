using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StorageAnalyzer.Core.Entities;

namespace StorageAnalyzer.Infrastructure.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<FolderEntity> Folders { get; set; }
    public DbSet<FileEntity> Files { get; set; }
    public DbSet<ScanSession> ScanSessions { get; set; }
    public DbSet<BackupLog> BackupLogs { get; set; }
    public DbSet<ScanLog> ScanLogs { get; set; }
    public DbSet<DuplicateSet> DuplicateSets { get; set; }
    public DbSet<Settings> Settings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Relație 1:1 între ApplicationUser și Settings
        builder.Entity<ApplicationUser>()
                .HasOne(u => u.Settings)
                .WithOne(s => s.User)
                .HasForeignKey<Settings>(s => s.Id)
                .OnDelete(DeleteBehavior.Cascade);

        // Relație 1:N între ApplicationUser și FolderEntity
        builder.Entity<FolderEntity>()
                .HasOne(f => f.User)
                .WithMany(u => u.Folders)
                .OnDelete(DeleteBehavior.Restrict);

        // Relație 1:N între ApplicationUser și ScanSession
        builder.Entity<ScanSession>()
                     .HasOne(ss => ss.User)
                     .WithMany(u => u.ScanSessions)
                     .HasForeignKey(ss => ss.UserId)
                     .OnDelete(DeleteBehavior.Restrict);

        // Relație 1:N între ApplicationUser și BackupLog
        builder.Entity<BackupLog>()
                .HasOne(b => b.User)
                .WithMany(u => u.BackupLogs)
                .OnDelete(DeleteBehavior.Restrict);

        // Relație 1:N între FolderEntity și FileEntity
        builder.Entity<FileEntity>()
                .HasOne(fe => fe.Folder)
                .WithMany(fo => fo.Files)
                .HasForeignKey(fe => fe.FolderId)
                .OnDelete(DeleteBehavior.Cascade);

        // Relație 1:N între DuplicateSet și FileEntity
        builder.Entity<FileEntity>()
                 .HasOne(f => f.DuplicateSet)
                 .WithMany(ds => ds.Files)
                 .HasForeignKey(f => f.DuplicateSetId)
                 .OnDelete(DeleteBehavior.SetNull);

        // Relație 1:N între ScanSession și ScanLog
        builder.Entity<ScanLog>()
               .HasOne(sl => sl.ScanSession)
               .WithMany(ss => ss.ScanLogs)
               .HasForeignKey(sl => sl.ScanSessionId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relație N:1 între ScanLog și FolderEntity
        builder.Entity<ScanLog>()
            .HasOne(s => s.Folder)
            .WithMany()
            .HasForeignKey(s => s.FolderId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
