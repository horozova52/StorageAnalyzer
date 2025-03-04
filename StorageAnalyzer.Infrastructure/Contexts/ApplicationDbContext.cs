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

        //FolderEntity & ApplicationUser 1-N (1 user poate avea mai multe foldere)
        builder.Entity<FolderEntity>()
     .HasOne(f => f.User)
     .WithMany(u => u.Folders)
     .HasForeignKey(f => f.UserId)
     .OnDelete(DeleteBehavior.Restrict);

        //FileEntity & FolderEntity 1-N (1 folder poate avea mai multe fisiere)
        builder.Entity<FileEntity>()
           .HasOne(fe => fe.Folder)
           .WithMany(fo => fo.Files)
           .HasForeignKey(fe => fe.FolderId)
           .OnDelete(DeleteBehavior.Cascade);

        // DuplicateSet & File 1-N (1 set de duplicate poate avea mai multe fisiere)
        builder.Entity<FileEntity>()
           .HasOne(f => f.DuplicateSet)
           .WithMany(ds => ds.Files)
           .HasForeignKey(f => f.DuplicateSetId)
           .OnDelete(DeleteBehavior.SetNull);

        // ScanSession & ApplicationUser 1-N (1 user poate avea mai multe sesiuni de scanare)
        builder.Entity<ScanSession>()
           .HasOne(ss => ss.User)
           .WithMany(u => u.ScanSessions)
           .HasForeignKey(ss => ss.UserId)
           .OnDelete(DeleteBehavior.Restrict);

        // ScanLog & FolderEntity & ScanSession N-1 (un log de scanare are un folder si o sesiune de scanare)
        builder.Entity<ScanLog>()
           .HasOne(sl => sl.Folder)
           .WithMany() // nu ai colectie de ScanLogs in FolderEntity
           .HasForeignKey(sl => sl.FolderId)
           .OnDelete(DeleteBehavior.Cascade);

        // ScanLog & ScanSession N-1 (un log de scanare are o sesiune de scanare)
        builder.Entity<ScanLog>()
           .HasOne(sl => sl.ScanSession)
           .WithMany(ss => ss.ScanLogs)
           .HasForeignKey(sl => sl.ScanSessionId)
           .OnDelete(DeleteBehavior.Cascade);

        // BackupLog & ApplicationUser 1-N (1 user poate avea mai multe backup-uri)
        builder.Entity<BackupLog>()
           .HasOne(b => b.User)
           .WithMany(u => u.BackupLogs)
           .HasForeignKey(b => b.UserId)
           .OnDelete(DeleteBehavior.Restrict);

        // Settings 1–1 => user nu poate avea multiple Settings
        builder.Entity<ApplicationUser>()
           .HasOne(u => u.Settings)
           .WithOne(s => s.User)
           .HasForeignKey<Settings>(s => s.UserId)
           .OnDelete(DeleteBehavior.Cascade);

        // Asigurare 1–1 => user nu poate avea multiple Settings
        builder.Entity<Settings>()
           .HasIndex(s => s.UserId)
           .IsUnique();

    }
}
