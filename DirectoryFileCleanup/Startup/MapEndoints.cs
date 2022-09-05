using DirectoryFileCleanup.Interfaces;
using DirectoryFileCleanup.Shared;
using DirectoryFileCleanup.Shared.Model;

namespace DirectoryFileCleanup.Startup
{
    public static class MapEndoints
    {
        public static WebApplication MapDirectoryEndpoints(this WebApplication app)
        {
            app.MapGet("/folderdirectory", GetDirectories);
            app.MapGet("/folderdirectory/{path}", GetDirectoryByPath);
            app.MapPost("/folderdirectory", AddFolderDirectory);
            app.MapPut("/folderdirectory", UpdateFolderDirectory);
            app.MapDelete("/folderdirectory/{id}", DeleteFolderDirectory);
            //app.MapGet("/folderdirectory2", GetDirectories2);
            return app;
        }

        private static async Task<IResult> AddFolderDirectory(FolderDirectory directory, IFolderDirectoryService folderDirectoryService)
        {
            return Results.Ok(folderDirectoryService.AddFolderDirectory(directory));
        }

        private static async Task<IResult> GetDirectories(IFolderDirectoryService folderDirectoryService)
        {
            return Results.Ok(folderDirectoryService.GetFileDirectories());
        }
        private static async Task<IResult> GetDirectoryByPath(string path, IFolderDirectoryService folderDirectoryService)
        {
            var response = folderDirectoryService.GetFileDirectory(path);
            if (response == null)
                return Results.NotFound();
            return Results.Ok(folderDirectoryService.GetFileDirectory(path));
        }

        private static async Task<IResult> UpdateFolderDirectory(FolderDirectory folderDirectory, IFolderDirectoryService folderDirectoryService)
        {
            return Results.Ok(folderDirectoryService.UpdateFolderDirectory(folderDirectory));
        }

        private static async Task<IResult> DeleteFolderDirectory(int id, IFolderDirectoryService folderDirectoryService)
        {
            return Results.Ok(folderDirectoryService.DeleteFolderDirectory(id));
        }

        //private static async Task<ServiceResponse<List<FolderDirectory>>> GetDirectories2(IFolderDirectoryService folderDirectoryService)
        //{
        //    return await folderDirectoryService.GetFileDirectories();
        //}
    }
}
