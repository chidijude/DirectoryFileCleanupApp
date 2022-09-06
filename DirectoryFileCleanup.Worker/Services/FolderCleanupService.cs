using DirectoryFileCleanup.Shared.Model;
using DirectoryFileCleanup.Shared.UnitOfWork;
using DirectoryFileCleanup.Worker.Interfaces;
using Serilog;

namespace DirectoryFileCleanup.Worker.Services;

public class FolderCleanupService : IFolderCleanupService
{
    private readonly IUnitOfWork _unit;
    private readonly ILogger<Worker> _logger;

    public FolderCleanupService(IUnitOfWork unit, ILogger<Worker> logger)
    {
        _unit = unit;
        _logger = logger;
    }
    public Task<bool> Clean(FolderDirectory folderDirectory)
    {
        if (!Directory.Exists(folderDirectory.Path))
        {
            Log.Information("'{Directory}' does not exist", folderDirectory.Path);
            return Task.FromResult(false);
        }
        DirectoryInfo di = new DirectoryInfo(folderDirectory.Path);
        if (folderDirectory.RetentionPeriod.HasValue)
        {
            DeleteFilesAndFoldersByDateUpdated(di, folderDirectory.RetentionPeriod.Value);
        }
        else
        {
            DeleteFilesAndFolders(di);
        }
        return Task.FromResult(true);
    }

    private void DeleteFilesAndFolders(DirectoryInfo di)
    {
        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            dir.Delete(true);
        }
    }
    private void DeleteFilesAndFoldersByDateUpdated(DirectoryInfo di, int retentionPeriod)
    {
        foreach (FileInfo file in di.GetFiles())
        {
            var lastAccessedDay =  file.LastAccessTime.Date;
            if (DateTime.Now.Date.AddDays(-1 * retentionPeriod) > lastAccessedDay)
                file.Delete();

        }
        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            var lastAccessedDay = dir.LastAccessTime.Date;
            if (DateTime.Now.Date.AddDays(-1 * retentionPeriod) > lastAccessedDay)
                dir.Delete();
        }
    }

    public List<FolderDirectory> GetDirectories()
    {
        return _unit.Directories.GetAll().ToList();
    }
}
