using System.Diagnostics;
using System.Text;
using Cowsay.Abstractions;
using Microsoft.AspNetCore.Mvc;
using TRBlog.Database;
using TRBlog.Models;

namespace TRBlog.Controllers;

public class AboutController : Controller
{
    private ICattleFarmer _cattleFarmer;
    private readonly ILogger<AboutController> _logger;

    public AboutController(ILogger<AboutController> logger, ICattleFarmer cattleFarmer)
    {
        _cattleFarmer = cattleFarmer;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        return View("~/Views/About.cshtml");
    }

    [HttpGet]
    public async Task<ContentResult> AboutSection()
    {
        var setting = await Setting.GetSetting();
        StringBuilder aboutSectionHtml = new StringBuilder();
        var reminderCow = await _cattleFarmer.RearCowAsync();
        string reminder = reminderCow.Say("You need to fill out your about section!", "Oo");

        if (setting.AboutSection?.Length > 0)
        {
            aboutSectionHtml.AppendFormat("<div>{0}</div>", setting.AboutSection);
        }
        else
        {
            aboutSectionHtml.Append("<div class='jumbotron container text-white ml-auto'>");
            aboutSectionHtml.AppendFormat("<pre>{0}</pre>", reminder);
            aboutSectionHtml.Append("</div>");
        }

        return new ContentResult()
        {
            Content = aboutSectionHtml.ToString(),
            ContentType = "text/html"
        };
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
