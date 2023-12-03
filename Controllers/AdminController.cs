using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRBlog.Database;
using TRBlog.Models;
using TRBlog.Models.Admin;

namespace TRBlog.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> logger)
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

    [Route("/Admin/Blogs")]
    [HttpGet]
    public async Task<IActionResult> AllBlogs()
    {
        var allBlogs = await Blog.GetAll();

        return View("~/Views/Admin/Blogs.cshtml", new AdminBlogsViewModel()
        {
            IsAuthenticated = false,
            AllBlogs = allBlogs
        });
    }

    [Route("/Admin/Blog/Update/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateBlog(int id)
    {
        return View("~/Views/Admin/EditBlog.cshtml", new EditBlogViewModel()
        {
            Blog = await Blog.GetBlogById(id),
             Categories = Category.GetAllCategories()
        });
    }

    [Route("/Admin/Blog/Deactivate/{id}")]
    [Authorize]
    public async Task<IActionResult> DeactivateBlog()
    {
        return View("~/Views/Admin/Blogs.cshtml");
    }

    [Route("/Admin/Blog/Activate/{id}")]
    [Authorize]
    public async Task<IActionResult> ActivateBlog()
    {
        return View("~/Views/Admin/Blogs.cshtml");
    }

    [Route("/Admin/Blog/Delete/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteBlog()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
