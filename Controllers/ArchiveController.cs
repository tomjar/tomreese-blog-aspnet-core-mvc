using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TRBlog.Models;

namespace TRBlog.Controllers;

public class ArchiveController : Controller
{
    private readonly ILogger<ArchiveController> _logger;

    public ArchiveController(ILogger<ArchiveController> logger)
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
