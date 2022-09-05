using DirectoryFileCleanup.Shared.Repository.Interface;

namespace DirectoryFileCleanup.Shared.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IFolderDirectoryRepository Directories { get; }
    int Complete();
}
