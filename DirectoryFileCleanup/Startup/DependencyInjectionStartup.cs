using DirectoryFileCleanup.Interfaces;
using DirectoryFileCleanup.Services;
using DirectoryFileCleanup.Shared.UnitOfWork;

namespace DirectoryFileCleanup.Startup;

public static class DependencyInjectionStartup
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFolderDirectoryService, FolderDirectoryService>();
        //services.AddDbContext<DataContext>(options =>
        //    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("DirectoryFileCleanup")));

        return services;
    }
}
