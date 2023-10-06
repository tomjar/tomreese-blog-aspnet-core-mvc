using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TRBlog.Models;

namespace TRBlog.Controllers;

public class AddBlogController : Controller
{
    private readonly ILogger<AddBlogController> _logger;

    public AddBlogController(ILogger<AddBlogController> logger)
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
