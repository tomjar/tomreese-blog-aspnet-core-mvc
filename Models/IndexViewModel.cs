using TRBlog.Database;

namespace TRBlog.Models;

public class IndexViewModel{
    
    public string Title {get;set;}
    public string Greeting {get;set;}

    public Blog[] LastThirtyDaysBlogs{get;set;} = Array.Empty<Blog>();
}