using System.Diagnostics;
using System.Text;
using Cowsay.Abstractions;
using Microsoft.AspNetCore.Mvc;
using TRBlog.Database;
using TRBlog.Models;

namespace TRBlog.Controllers;

public class BlogsController : Controller
{
    private ICattleFarmer _cattleFarmer;
    private readonly ILogger<BlogsController> _logger;

    public BlogsController(ILogger<BlogsController> logger, ICattleFarmer cattleFarmer)
    {
        _cattleFarmer = cattleFarmer;
        _logger = logger;
    }

    [Route("Blog")]
    [Route("Blog/Index")]
    [Route("Blog/{name}")]
    [Route("Blogs")]
    [Route("Blogs/Index")]
    [Route("Blogs/{name}")]
    [HttpGet]
    public async Task<IActionResult> Index(string name)
    {
        var blog = await Blog.GetBlogByName(name);

        if (blog?.Id > 0)
        {
            return View("~/Views/Blog.cshtml", new BlogViewModel()
            {
                Body = blog.Body,
                Category = blog.Category,
                Createtimestamp = blog.Createtimestamp,
                Header = blog.Header,
                Modifytimestamp = blog.Modifytimestamp
            });
        }
        else
        {
            var missingCow = await _cattleFarmer.RearCowAsync();
            string blogMissing = missingCow.Say("This is not the blog you are looking for.", "Oo");
            StringBuilder notFoundBlogHtml = new StringBuilder();
            notFoundBlogHtml.Append("<div class='jumbotron container text-white ml-auto'>");
            notFoundBlogHtml.AppendFormat("<pre>{0}</pre>", blogMissing);
            notFoundBlogHtml.Append("</div>");

            return View("~/Views/Blog.cshtml", new BlogViewModel()
            {
                Body = notFoundBlogHtml.ToString(),
                Category = string.Empty,
                Createtimestamp = DateTime.UtcNow,
                Header = "Not Found",
                Modifytimestamp = DateTime.UtcNow
            });
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
