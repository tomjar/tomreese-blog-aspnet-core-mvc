using TRBlog.Database;

namespace TRBlog.Models;

public class ArchiveViewModel
{
    public string EmptyArchiveMessage { get; set; }
    public Dictionary<int, Blog[]> YearAndBlogs { get; set; }
}