using DirectoryFileCleanup.Shared.Model;

namespace DirectoryFileCleanup.Shared.Repository.Interface;

public interface IFolderDirectoryRepository : IRepository<FolderDirectory>
{
    FolderDirectory GetByPath(string path);
}
