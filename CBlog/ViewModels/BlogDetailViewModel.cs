using System.ComponentModel.DataAnnotations;
using CBlog.Models;

namespace CBlog.ViewModels
{
    public class BlogDetailViewModel
    {
        public int BlogId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public IEnumerable<Comment>? Comments { get; set; }
        public IEnumerable<React>? Reacts { get; set; }
    }
}
