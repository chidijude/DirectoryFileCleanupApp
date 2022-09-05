using DirectoryFileCleanup.Shared.Data;
using DirectoryFileCleanup.Shared.Model;
using DirectoryFileCleanup.Shared.Repository.Interface;

namespace DirectoryFileCleanup.Shared.Repository.Implementation;

public class FolderDirectoryRepository : Repository<FolderDirectory>, IFolderDirectoryRepository
{
    public FolderDirectoryRepository(DataContext context)
        : base(context)
    {
    }
    public DataContext dataContext
    {
        get { return _context as DataContext; }
    }

    public FolderDirectory GetByPath(string path)
    {
        return dataContext.Directories.Where(d => d.Path.ToLower().Equals(path.ToLower())).FirstOrDefault();
    }  
}
