using TRBlog.Database;

namespace TRBlog.Models.Admin;

public class EditBlogViewModel {
    public Blog Blog {get;set;}
    public Category[] Categories {get;set;}
}