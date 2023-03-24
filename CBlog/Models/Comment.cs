using System.ComponentModel.DataAnnotations;

namespace CBlog.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int BlogId { get; set; }
		public Blog Blog { get; set; }
	}
}
