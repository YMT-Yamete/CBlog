using System.Diagnostics;
using System.Security.Claims;
using CBlog.Data;
using CBlog.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

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

        public IActionResult Index(int? page = 1)
        {
            if (page != null && page < 1)
            {
                page = 1;
            }

            var pageSize = 6;
            IPagedList<Blog> Blogs = context.Blog.ToList().ToPagedList(page ?? 1, pageSize);
            ViewData["BlogGridPartial"] = "All Blogs";
            return View(Blogs);
        }

        public IActionResult ReturnAllBlogsInPartial(int? page = 1)
        {
            if (page != null && page < 1)
            {
                page = 1;
            }
            var pageSize = 6;
            IPagedList<Blog> Blogs = context.Blog.ToList().ToPagedList(page ?? 1, pageSize);
            ViewData["BlogGridPartial"] = "All Blogs";
            return PartialView("_BlogGridPartial", Blogs);
        }

        public IActionResult ReturnMyBlogsInPartial(int? page = 1)
        {
            if (page != null && page < 1)
            {
                page = 1;
            }
            var pageSize = 6;
            IPagedList<Blog> Blogs = context.Blog
                .Where(b => b.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .ToList().ToPagedList(page ?? 1, pageSize);
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