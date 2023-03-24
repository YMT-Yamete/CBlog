using CBlog.Data.Enum;

namespace CBlog.Models
{
    public class React
    {
        public int Id { get; set; }
        public ReactType GivenReact { get; set; }
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int BlogId { get; set; }
		public Blog Blog { get; set; }
	}
}
