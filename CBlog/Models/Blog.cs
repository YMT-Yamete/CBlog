using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBlog.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }
        
        public string? Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
		public ApplicationUser? ApplicationUser { get; set; }
		public IEnumerable<Comment>? Comments { get; set; }
		public IEnumerable<React>? Reacts { get; set; }
	}
}
