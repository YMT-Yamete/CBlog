using System.Security.Claims;
using CBlog.Data;
using CBlog.Data.Enum;
using CBlog.Models;
using CBlog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CBlog.Controllers
{
    public class ReactController : Controller
    {
        private readonly ApplicationDbContext context;

        public ReactController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult GiveReact(int BlogId, string React)
        {
            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ReactType reactType = React == "Like" ? ReactType.Like : ReactType.Dislike;
            bool alreadyReacted = context.React.Where(r => r.BlogId == BlogId && r.ApplicationUserId == currentUserId).Any();
            if (alreadyReacted)
            {
                React reactToRemove = context.React.FirstOrDefault(r => r.BlogId == BlogId && r.ApplicationUserId == currentUserId);
                React newReact = new React()
                {
                    GivenReact = reactType,
                    ApplicationUserId = currentUserId,
                    BlogId = BlogId
                };
                if(reactToRemove.GivenReact == newReact.GivenReact)
                {
                    context.React.Remove(reactToRemove);
                }
                else
                {
                    context.React.Remove(reactToRemove);
                    context.React.Add(newReact);
                }
                context.SaveChanges();
            }
            else
            {
                React react = new React()
                {
                    GivenReact = reactType,
                    ApplicationUserId = currentUserId,
                    BlogId = BlogId
                };
                context.React.Add(react);
                context.SaveChanges();
            }
            Blog? blog = context.Blog
                .Include(b => b.Reacts)!
                .ThenInclude(r => r.ApplicationUser)
                .FirstOrDefault(b => b.Id == BlogId);

            BlogDetailViewModel model = new BlogDetailViewModel()
            {
                Reacts = blog!.Reacts,
            };
            return PartialView("~/Views/React/_ReactPartial.cshtml", model);
        }
    }
}
