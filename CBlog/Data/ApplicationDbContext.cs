using CBlog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CBlog.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Blog> Blog { get; set; }
		public DbSet<Comment> Comment { get; set; }
		public DbSet<React> React { get; set; }
	}
}
