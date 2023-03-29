using CBlog.Data;
using CBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CBlog.Controllers
{
    [Authorize]
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
			IEnumerable<Blog> Blogs = context.Blog
				.Where(b => b.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
				.ToList();
			return View(Blogs);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Blog model, IFormFile image)
        {
			if(!ModelState.IsValid)
            {
				return View();
            }
			model.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			model.CreatedDate = DateTime.Now;

			var fileName = DateTime.Now.ToString("M-d-yyyy") + Path.GetFileName(image.FileName);
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imgs", fileName);
			using var stream = new FileStream(path, FileMode.Create);
			image.CopyTo(stream);
			model.Image = fileName;

			context.Blog.Add(model);
			context.SaveChanges();
			return RedirectToAction("Index", "Blog");
        }

		[HttpGet]
		public IActionResult Detail(int Id)
		{
			Blog blog = context.Blog.Include(b => b.ApplicationUser).FirstOrDefault(b => b.Id == Id);
			return View(blog);
		}

		[HttpPost]
		public IActionResult Delete(int BlogId)
        {
			Blog blog = context.Blog.Find(BlogId);
			context.Blog.Remove(blog);
			context.SaveChanges();
			return RedirectToAction("Index", "Blog");
        }
	}
}
