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


    public IActionResult Index()
    {
        return View("~/Views/Blog.cshtml");
    }

    [HttpGet]
    public async Task<ContentResult> GetBlogByName(string name)
    {
        var blog = await Blog.GetBlogByName(name);
        var missingCow = await _cattleFarmer.RearCowAsync();
        string blogMissing = missingCow.Say("This is not the blog you are looking for.", "Oo");
        StringBuilder blogHtml = new StringBuilder();

        if (blog != null)
        {
            blogHtml.AppendFormat("<h1 class='text-white'>{0}</h1>", blog.Header);
            blogHtml.Append("<ul>");
            blogHtml.Append("<li>");
            blogHtml.AppendFormat("<img src='/images/{0}' alt='' width='75px' height='75px' />", blog.Category);
            blogHtml.Append("</li>");
            blogHtml.Append("<li>");
            blogHtml.AppendFormat("<span class='text-small'>by <a href='/about'>Thomas Reese</a></span>");
            blogHtml.Append("</li>");
            blogHtml.Append("<li class='list-group-item border-0 text-white'>");
            blogHtml.Append("<span style='font-size:xx-small;'>");
            blogHtml.AppendFormat("<i>first published on @Model.createtimestamp UTC</i>");
            blogHtml.Append("<br />");
            if (blog.Modifytimestamp.HasValue)
            {
                blogHtml.AppendFormat("<i>last updated on {0} UTC</i>", blog.Modifytimestamp);
            }
            blogHtml.Append("</span>");
            blogHtml.Append("</li>");
            blogHtml.Append("</ul>");
            blogHtml.Append("<div class='text-white shadow-lg rounded flex-fill flex-xl-grow-1'>");
            blogHtml.Append(blog.Body);
            blogHtml.Append("</p>");
        }
        else
        {
            blogHtml.Append("<div class='jumbotron container text-white ml-auto'>");
            blogHtml.AppendFormat("<pre>{0}</pre>", blogMissing);
            blogHtml.Append("</div>");
        }

        return new ContentResult()
        {
            Content = blogHtml.ToString(),
            ContentType = "text/html"
        };
    }

    [HttpGet]
    public async Task<ContentResult> LastThirtyDaysBlogs()
    {
        var lastThirtyDays = await Blog.GetAllPublishedLastThirtyDays();
        var greetingCow = await _cattleFarmer.RearCowAsync();
        string greeting = greetingCow.Say("Its rather empty around here, check out the Archive for past blogs.", "Oo");
        StringBuilder lastThirtyDaysHtml = new StringBuilder();
        if (lastThirtyDays.Length > 0)
        {
            lastThirtyDaysHtml.Append("<div class='d-flex flex-wrap text-white flex-row bd-highlight mb-3'>");
            foreach (var item in lastThirtyDays)
            {
                lastThirtyDaysHtml.Append("<div class='p-1 bd-highlight'>");
                lastThirtyDaysHtml.Append("<div class='card bg-dark'>");
                lastThirtyDaysHtml.Append("<div class='card-body'>");
                lastThirtyDaysHtml.Append("<h3 class='card-title'></h3>");
                lastThirtyDaysHtml.Append("<span>");
                lastThirtyDaysHtml.AppendFormat("<a href='/blog/{0}' title='{1}'>", item.Name, item.Header);
                lastThirtyDaysHtml.Append(item.Header);
                lastThirtyDaysHtml.Append("</a>");
                lastThirtyDaysHtml.Append("</span>");
                lastThirtyDaysHtml.Append("<br />");
                lastThirtyDaysHtml.Append("<span class='bg-dark'>");
                lastThirtyDaysHtml.Append("<ul class='list-group list-group-horizontal bg-dark'>");
                lastThirtyDaysHtml.Append("<li class='list-group-item border-0 bg-dark'>");
                lastThirtyDaysHtml.Append("<span class='text-tiny text-white'>by <a href='/about'>Thomas Reese</a></span>");
                lastThirtyDaysHtml.Append("</li>");
                lastThirtyDaysHtml.Append("<li class='list-group-item border-0 bg-dark text-white'>");
                lastThirtyDaysHtml.AppendFormat("<i class='text-tiny'>first published on {0} UTC</i>", item.Createtimestamp);
                lastThirtyDaysHtml.Append("<br />");

                if (item.Modifytimestamp.HasValue)
                {
                    lastThirtyDaysHtml.AppendFormat("<i class='text-tiny'>last updated on {0} UTC</i>", item.Modifytimestamp.Value);
                }

                lastThirtyDaysHtml.Append("</li>");
                lastThirtyDaysHtml.Append("</ul>");
                lastThirtyDaysHtml.Append("</span>");
                lastThirtyDaysHtml.Append("</div>");
                lastThirtyDaysHtml.Append("</div>");
                lastThirtyDaysHtml.Append("</div>");
            }
            lastThirtyDaysHtml.Append("</div>");
        }
        else
        {
            lastThirtyDaysHtml.Append("<div class='jumbotron container text-white ml-auto'>");
            lastThirtyDaysHtml.AppendFormat("<pre>{0}</pre>", greeting);
            lastThirtyDaysHtml.Append("</div>");
        }
        return new ContentResult()
        {
            Content = lastThirtyDaysHtml.ToString(),
            ContentType = "text/html"
        };
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
