using System.Security.Claims;
using CBlog.Data;
using CBlog.Models;
using CBlog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext context;

        public CommentController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult ReturnCommentListPartial(int BlogId, string Comment)
        {
            Comment comment = new Comment()
            {
                Content = Comment,
                CreatedDate = DateTime.Now,
                ApplicationUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                BlogId = BlogId,
            };
            context.Comment.Add(comment);
            context.SaveChanges();

            Blog? blog = context.Blog
                .Include(b => b.ApplicationUser)
                .Include(b => b.Comments)!
                .ThenInclude(c => c.ApplicationUser)
                .Include(b => b.Reacts)
                .FirstOrDefault(b => b.Id == BlogId);
            BlogDetailViewModel blogDetail = new BlogDetailViewModel
            {
                CreatedDate = blog!.CreatedDate,
                Comments = blog.Comments,
            };
            return PartialView("~/Views/Comment/_CommentListPartial.cshtml", blogDetail);
        }
    }
}
