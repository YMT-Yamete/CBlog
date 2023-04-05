using CBlog.Data;
using CBlog.Models;
using CBlog.ViewModels;
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
			Blog? blog = context.Blog
				.Include(b => b.ApplicationUser)
				.Include(b => b.Comments)!
				.ThenInclude(c => c.ApplicationUser)
				.Include(b => b.Reacts)
				.FirstOrDefault(b => b.Id == Id);
			BlogDetailViewModel blogDetail = new BlogDetailViewModel
			{
				BlogId = blog!.Id,
				Title = blog.Title,
				Description = blog.Description,
				Content = blog.Content,
				Image = blog.Image,
				CreatedDate = blog.CreatedDate,
				ApplicationUserId = blog.ApplicationUserId,
				ApplicationUser = blog.ApplicationUser,
				Comments = blog.Comments,
				Reacts = blog.Reacts,
			};
			return View(blogDetail);
		}

		[HttpGet]
		public IActionResult Edit(int BlogId)
		{
			Blog model = context.Blog.FirstOrDefault(Blog => Blog.Id == BlogId);	
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(Blog model)
		{
			context.Blog.Update(model);
			context.SaveChanges();
            return RedirectToAction("Index", "Blog");
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
