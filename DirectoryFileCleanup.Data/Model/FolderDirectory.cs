using System.ComponentModel.DataAnnotations;

namespace DirectoryFileCleanup.Shared.Model;

public class FolderDirectory
{
    public int Id { get; set; }
    [Required]
    public string Path { get; set; } = string.Empty;
    public int? RetentionPeriod { get; set; }
}
