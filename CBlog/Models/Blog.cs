using System.ComponentModel.DataAnnotations;

namespace CBlog.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        public string Image { get; set; } = "https://t3.ftcdn.net/jpg/02/48/42/64/360_F_248426448_NVKLywWqArG2ADUxDq6QprtIzsF82dMF.jpg";

        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
		public ApplicationUser User { get; set; }
		public IEnumerable<Comment> Comments { get; set; }
		public IEnumerable<React> Reacts { get; set; }
	}
}
