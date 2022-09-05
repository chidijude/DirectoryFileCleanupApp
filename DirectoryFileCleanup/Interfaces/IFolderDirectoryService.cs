using DirectoryFileCleanup.Shared;
using DirectoryFileCleanup.Shared.Model;

namespace DirectoryFileCleanup.Interfaces
{
    public interface IFolderDirectoryService
    {
        Task<ServiceResponse<FolderDirectory>> AddFolderDirectory(FolderDirectory folderDirectory);
        Task<ServiceResponse<FolderDirectory>> UpdateFolderDirectory(FolderDirectory folderDirectory);
        Task<ServiceResponse<bool>> DeleteFolderDirectory(int id);
        Task<ServiceResponse<List<FolderDirectory>>> GetFileDirectories();
        Task<ServiceResponse<FolderDirectory>> GetFileDirectory(string path);
    }
}
