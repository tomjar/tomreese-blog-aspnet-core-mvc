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

    [Route("Archive")]
    [Route("Archive/Index")]
    [Route("Archives")]
    [Route("Archives/Index")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var yearAndBlogs = await Blog.GetAllArchived();

        if (yearAndBlogs.Count() > 0)
        {
            return View("~/Views/Archive.cshtml", new ArchiveViewModel()
            {
                YearAndBlogs = yearAndBlogs
            });
        }
        else
        {
            var archiveEmptyCow = await _cattleFarmer.RearCowAsync();
            string emptyArchive = archiveEmptyCow.Say("Add some blogs! Get to it chop chop!", "Oo");

            StringBuilder emptyArchiveHtml = new StringBuilder();
            emptyArchiveHtml.Append("<div class='jumbotron container text-white ml-auto'>");
            emptyArchiveHtml.AppendFormat("<pre>{0}</pre>", emptyArchive);
            emptyArchiveHtml.Append("</div>");

            return View("~/Views/Archive.cshtml", new ArchiveViewModel()
            {
                YearAndBlogs = new Dictionary<int, Blog[]>(),
                EmptyArchiveMessage = emptyArchiveHtml.ToString()

            });
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
