using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TRBlog.Models;

namespace TRBlog.Controllers;

public class IndexController : Controller
{
    private readonly ILogger<IndexController> _logger;


    public IndexController(ILogger<IndexController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {

        CommonViewModel model = new CommonViewModel()
        {
            Title = "Welcome to tomreese.blog!",
            IsAuthenticated = false
        };

        return View("~/Views/Index.cshtml", model);
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
