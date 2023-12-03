using TRBlog.Database;

namespace TRBlog.Models.Admin;

public class AdminBlogsViewModel  {
    public bool IsAuthenticated{get;set;}
    public Blog[] AllBlogs{get;set;}
    
}