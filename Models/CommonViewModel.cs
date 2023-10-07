using TRBlog.Database;

namespace TRBlog.Models;

public class CommonViewModel{

    public bool IsAuthenticated {get;set;}
    public string Title {get;set;} = "";
}