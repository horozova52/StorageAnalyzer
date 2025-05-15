// Program.cs  –  StorageAnalyzer.Server
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MudBlazor.Services;
using Serilog;
using StorageAnalyzer.Core.Entities;
using StorageAnalyzer.Infrastructure.Cash;
using StorageAnalyzer.Infrastructure.Logging;
using StorageAnalyzer.Infrastructure.Contexts;
using StorageAnalyzer.Infrastructure.Repositories;
using StorageAnalyzer.Infrastructure.Repositories.Backup;
using StorageAnalyzer.Infrastructure.Repositories.File;
using StorageAnalyzer.Infrastructure.Repositories.Folder;
using StorageAnalyzer.Infrastructure.Services.Factories;
using StorageAnalyzer.Infrastructure.Services.FileAccessor;
using StorageAnalyzer.Infrastructure.Services.Interfaces;
using StorageAnalyzer.Infrastructure.Services.WMI;
using StorageAnalyzer.Server.Components;
using StorageAnalyzer.Server.Components.Account;
using StorageAnalyzer.UseCases.Features.Backups.Handlers;

#region ───────── 1. Serilog early-init ─────────
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.File("Logs/app-.log", rollingInterval: RollingInterval.Day)
    .WriteTo.Console()
    .CreateLogger();

#endregion

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();    


#region ───────── 2. MVC / Blazor / Swagger ─────────
builder.Services
    .AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMudServices();
builder.Services.AddHttpContextAccessor();
#endregion

#region ───────── 3. Identity & Auth ─────────
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies();

builder.Services.AddAuthorization();

builder.Services.AddIdentityCore<ApplicationUser>(o => o.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IEmailSender<ApplicationUser>, SmtpEmailSender>();
#endregion

#region ───────── 4. Database ─────────
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? throw new InvalidOperationException("No conn-string");
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(cs));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
#endregion

#region ───────── 5. MediatR  +  Pipeline Logging ─────────
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<BackupHandler>());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>),
    typeof(StorageAnalyzer.Infrastructure.Logging.LoggingBehavior<,>));
#endregion

#region ───────── 6. Factories / Services / Cache / Proxy ─────────
builder.Services.AddSingleton<ScanCacheService>();


builder.Services.AddScoped<IServiceFactory, DefaultServiceFactory>();
builder.Services.AddScoped<AdvancedServiceFactory>();

builder.Services.AddScoped<IBackupRepository, BackupRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IScanRepository, ScanRepository>();
builder.Services.AddScoped<IFolderRepository, FolderRepository>();
builder.Services.AddScoped<IDiskInfoService, DiskInfoService>();

builder.Services.AddMemoryCache();

// File Accessor Proxy 
builder.Services.AddScoped<FileAccessor>();
builder.Services.AddScoped<IFileAccessor>(sp =>
{
    var real = sp.GetRequiredService<FileAccessor>();
    var cache = sp.GetRequiredService<IMemoryCache>();
    var log = sp.GetRequiredService<ILogger<FileAccessorProxy>>();
    return new FileAccessorProxy(real, cache, log);
});
#endregion

#region ───────── 7. Identity Razor helpers ─────────
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
#endregion

var app = builder.Build();

#region ───────── 8. Middleware pipeline ─────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = "swagger";
    });
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveWebAssemblyRenderMode()
   .AddAdditionalAssemblies(typeof(StorageAnalyzer.Client._Imports).Assembly);

app.MapAdditionalIdentityEndpoints();
app.MapControllers();
#endregion

app.Run();
