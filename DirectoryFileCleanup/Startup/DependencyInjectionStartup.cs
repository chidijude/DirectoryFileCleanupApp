using DirectoryFileCleanup.Interfaces;
using DirectoryFileCleanup.Services;
using DirectoryFileCleanup.Shared.UnitOfWork;

namespace DirectoryFileCleanup.Startup;

public static class DependencyInjectionStartup
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, WebApplicationBuilder builder )
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFolderDirectoryService, FolderDirectoryService>();

        services.AddDbContext<DataContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("DirectoryFileCleanup")));

        return services;
    }
}
