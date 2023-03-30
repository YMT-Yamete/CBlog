using System.Diagnostics;
using System.Security.Claims;
using CBlog.Data;
using CBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace CBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Blog> Blogs = context.Blog.ToList();
            ViewData["BlogGridPartial"] = "All Blogs";
            return View(Blogs);
        }

        public IActionResult ReturnAllBlogsInPartial()
        {
            IEnumerable<Blog> Blogs = context.Blog.ToList();
            ViewData["BlogGridPartial"] = "All Blogs";
            return PartialView("_BlogGridPartial", Blogs);
        }

        public IActionResult ReturnMyBlogsInPartial()
        {
            IEnumerable<Blog> Blogs = context.Blog
                .Where(b => b.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .ToList();
            ViewData["BlogGridPartial"] = "My Blogs";
            return PartialView("_BlogGridPartial", Blogs);
        }

        public IActionResult About()
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
}