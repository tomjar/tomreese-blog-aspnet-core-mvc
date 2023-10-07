using System.Diagnostics;
using System.Text;
using Cowsay.Abstractions;
using Microsoft.AspNetCore.Mvc;
using TRBlog.Database;
using TRBlog.Models;

namespace TRBlog.Controllers;

public class ArchiveController : Controller
{
    private ICattleFarmer _cattleFarmer;
    private readonly ILogger<ArchiveController> _logger;

    public ArchiveController(ILogger<ArchiveController> logger, ICattleFarmer cattleFarmer)
    {
        _cattleFarmer = cattleFarmer;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("~/Views/Archive.cshtml");
    }

    [HttpGet]
    public async Task<ContentResult> YearAndBlogs()
    {
        StringBuilder yearAndBlogsHtml = new StringBuilder();

        var yearAndBlogs = await Blog.GetAllArchived();
        var archiveEmptyCow = await _cattleFarmer.RearCowAsync();
        string emptyArchive = archiveEmptyCow.Say("Add some blogs! Get to it chop chop!", "Oo");

        if (yearAndBlogs.Count() > 0)
        {
            yearAndBlogsHtml.Append("<div class='archive text-white'>");

            foreach (var item in yearAndBlogs)
            {
                yearAndBlogsHtml.AppendFormat("<strong>{0}</strong>", item.Key);
                yearAndBlogsHtml.Append("<br />");
                foreach (var blog in item.Value)
                {
                    yearAndBlogsHtml.AppendFormat("<p title='{0}'>", blog.Description);
                    yearAndBlogsHtml.AppendFormat("<a href='/blog/{0}'>", blog.Name);
                    yearAndBlogsHtml.AppendFormat("<small>{0}</small>", blog.Header);
                    yearAndBlogsHtml.Append("</a>");
                    yearAndBlogsHtml.Append("</p>");
                }
            }

            yearAndBlogsHtml.Append("</div>");
        }
        else
        {
            yearAndBlogsHtml.Append("<div class='jumbotron container text-white ml-auto'>");
            yearAndBlogsHtml.AppendFormat("<pre>{0}</pre>", emptyArchive);
            yearAndBlogsHtml.Append("</div>");
        }

        return new ContentResult()
        {
            Content = yearAndBlogsHtml.ToString(),
            ContentType = "text/html"
        };
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
