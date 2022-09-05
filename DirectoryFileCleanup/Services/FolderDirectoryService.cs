using DirectoryFileCleanup.Interfaces;
using DirectoryFileCleanup.Shared;
using DirectoryFileCleanup.Shared.Model;
using DirectoryFileCleanup.Shared.UnitOfWork;

namespace DirectoryFileCleanup.Services
{
    public class FolderDirectoryService : IFolderDirectoryService
    {
        private readonly IUnitOfWork _unit;

        public FolderDirectoryService(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<ServiceResponse<FolderDirectory>> AddFolderDirectory(FolderDirectory folderDirectory)
        {
            if (!Directory.Exists(folderDirectory.Path))
                return new ServiceResponse<FolderDirectory> { Success = false, Message = "Invalid path" };

            _unit.Directories.Add(folderDirectory);
            _unit.Complete();

            return new ServiceResponse<FolderDirectory> { Data = folderDirectory };

        }

        public async Task<ServiceResponse<List<FolderDirectory>>> GetFileDirectories()
        {
            var response = _unit.Directories.GetAll().ToList();
            return new ServiceResponse<List<FolderDirectory>> { Data = response };
        }

        public async Task<ServiceResponse<FolderDirectory>> GetFileDirectory(string path)
        {
            var response = _unit.Directories.GetByPath(path);
            return new ServiceResponse<FolderDirectory> { Data = response };
        }

        public async Task<ServiceResponse<FolderDirectory>> UpdateFolderDirectory(FolderDirectory folderDirectory)
        {
            var response = new ServiceResponse<FolderDirectory>();

            var result = _unit.Directories.Get(folderDirectory.Id);
            if (result == null)
            {
                response.Success = false;
                response.Message = "No such path was found";
                return response;
            }
            if (!Directory.Exists(folderDirectory.Path))
                return new ServiceResponse<FolderDirectory> { Success = false, Message = "Invalid path" };

            result.RetentionPeriod = folderDirectory.RetentionPeriod;
            result.Path = folderDirectory.Path;
            _unit.Complete();
            response.Data = result;
            return response;
        }
      
        public async Task<ServiceResponse<bool>> DeleteFolderDirectory(int id)
        {
            var response = new ServiceResponse<bool>();

            var result = _unit.Directories.Get(id);
            if (result == null)
            {
                response.Success = false;
                response.Message = "Directory not found";
                return response;
            }
            _unit.Directories.Remove(result);           
            _unit.Complete();
            response.Data = true;
            return response;
        }
    }
}
