using System.Diagnostics;
using System.Text;
using Cowsay.Abstractions;
using Microsoft.AspNetCore.Mvc;
using TRBlog.Database;
using TRBlog.Models;

namespace TRBlog.Controllers;

public class IndexController : Controller
{
    private readonly ILogger<IndexController> _logger;
    private ICattleFarmer _cattleFarmer;


    public IndexController(ILogger<IndexController> logger, ICattleFarmer cattleFarmer)
    {
        _cattleFarmer = cattleFarmer;
        _logger = logger;
    }

    [Route("Index")]
    [Route("Home")]
    [Route("/")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var lastThirtyDays = await Blog.GetAllPublishedLastThirtyDays();

        if (lastThirtyDays.Length > 0)
        {
            return View("~/Views/Index.cshtml", new CommonViewModel()
            {
                IsAuthenticated = false,
                LastThirtyDaysBlogs = lastThirtyDays.Select(b => new BlogViewModel()
                {
                    Name= b.Name,
                    Body = b.Body,
                    Category = b.Category,
                    Createtimestamp = b.Createtimestamp,
                    Modifytimestamp = b.Modifytimestamp,
                    Header = b.Header
                }).ToArray(),
                Title = "Welcome to tomreese.blog!"
            });
        }
        else
        {
            var greetingCow = await _cattleFarmer.RearCowAsync();
            string greeting = greetingCow.Say("Its rather empty around here, check out the Archive for past blogs.", "Oo");
            StringBuilder emptyLastThirtyDaysMessage = new StringBuilder();

            emptyLastThirtyDaysMessage.Append("<div class='jumbotron container text-white ml-auto'>");
            emptyLastThirtyDaysMessage.AppendFormat("<pre>{0}</pre>", greeting);
            emptyLastThirtyDaysMessage.Append("</div>");

            return View("~/Views/Index.cshtml", new CommonViewModel()
            {
                EmptyGreeting = emptyLastThirtyDaysMessage.ToString(),
                IsAuthenticated = false,
                LastThirtyDaysBlogs = Array.Empty<BlogViewModel>(),
                Title = "Welcome to tomreese.blog!"
            });
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
