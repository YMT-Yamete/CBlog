using Microsoft.AspNetCore.Identity;

namespace CBlog.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
		public IEnumerable<Blog> Blogs { get; set; }
		public IEnumerable<Comment> Comments { get; set; }
		public IEnumerable<React> Reacts { get; set; }
	}
}
