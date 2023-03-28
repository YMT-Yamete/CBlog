using CBlog.Data;
using CBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace CBlog.Controllers
{
	public class BlogController : Controller
	{
        private readonly ApplicationDbContext context;

        public BlogController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
		public IActionResult Index()
		{
			IEnumerable<Blog> Blogs = context.Blog.ToList();
			return View(Blogs);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Blog model)
        {
			if(!ModelState.IsValid)
            {
				return View();
            }
			model.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			model.CreatedDate = DateTime.Now;
			context.Blog.Add(model);
			context.SaveChanges();
			return RedirectToAction("Index", "Blog");
        }
	}
}
