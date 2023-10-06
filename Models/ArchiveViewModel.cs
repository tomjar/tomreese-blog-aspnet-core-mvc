using TRBlog.Database;

namespace TRBlog.Models;

public class ArchiveViewModel{
    public Dictionary<int, Blog[]> YearAndBlogs {get;set;}
}