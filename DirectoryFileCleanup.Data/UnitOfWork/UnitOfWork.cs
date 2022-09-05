using DirectoryFileCleanup.Shared.Data;
using DirectoryFileCleanup.Shared.Repository.Implementation;
using DirectoryFileCleanup.Shared.Repository.Interface;

namespace DirectoryFileCleanup.Shared.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public UnitOfWork(DataContext dataContext)
    {
        _context = dataContext;
        Directories = new FolderDirectoryRepository(_context);
    }
    public IFolderDirectoryRepository Directories { get; private set; }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
