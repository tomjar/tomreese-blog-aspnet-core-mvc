namespace TRBlog.Models;

public class BlogViewModel
{
    public string Name {get;set;}
    public string CategoryImageUrl
    {
        get
        {
            return string.Format("/images/{0}.png", Category);
        }
    }
    public string Author { get; set; } = "Thomas Reese";
    public string Header { get; set; }
    public string Category { get; set; }
    public string Body { get; set; }
    public DateTime Createtimestamp { get; set; }
    public DateTime? Modifytimestamp { get; set; }
}