using TRBlog.Database;

namespace TRBlog.Models;

public class CommonViewModel{

    public string EmptyGreeting {get;set;}
    public BlogViewModel[] LastThirtyDaysBlogs {get;set;}
    public bool IsAuthenticated {get;set;}
    public string Title {get;set;} = "";
}