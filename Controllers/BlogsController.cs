using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TRBlog.Models;

namespace TRBlog.Controllers;

public class BlogsController : Controller
{
    private readonly ILogger<BlogsController> _logger;

    public BlogsController(ILogger<BlogsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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
