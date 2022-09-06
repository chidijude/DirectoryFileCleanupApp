using DirectoryFileCleanup.Shared.Data;
using DirectoryFileCleanup.Shared.UnitOfWork;
using DirectoryFileCleanup.Worker;
using DirectoryFileCleanup.Worker.Interfaces;
using DirectoryFileCleanup.Worker.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.File(@"C:\Logs\FolderCleanup\Serilog",
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {Properties:j}  {Message:lj}{NewLine}{Exception}")
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    Log.Information("Starting Service");
    IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IFolderCleanupService, FolderCleanupService>();
        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<DataContext>(options =>
            options.UseSqlite(hostContext.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("DirectoryFileCleanup.Worker")), ServiceLifetime.Singleton, ServiceLifetime.Singleton);
    })
    .UseSerilog()
    .Build();

    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "There was a fatal error starting the service");
}
finally
{
    Log.CloseAndFlush();
}


