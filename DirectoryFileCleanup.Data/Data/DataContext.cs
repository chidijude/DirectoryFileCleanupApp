using DirectoryFileCleanup.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace DirectoryFileCleanup.Shared.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }


    public DbSet<FolderDirectory> Directories { get; set; }
}
