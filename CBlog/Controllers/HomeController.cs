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
            return View(Blogs);
        }

        public IActionResult My_Blogs()
        {
            IEnumerable<Blog> Blogs = context.Blog
                .Where(b => b.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .ToList();
            return View(Blogs);
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