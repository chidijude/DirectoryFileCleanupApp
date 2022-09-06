using DirectoryFileCleanup.Shared.Model;

namespace DirectoryFileCleanup.Worker.Interfaces;

public interface IFolderCleanupService
{
    List<FolderDirectory> GetDirectories();
    Task<bool> Clean(FolderDirectory folderDirectory);
}
