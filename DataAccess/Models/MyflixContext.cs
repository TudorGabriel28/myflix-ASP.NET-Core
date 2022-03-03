

using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
	public partial class MyflixContext : DbContext
	{
		public MyflixContext() : base()
		{
		}

		public MyflixContext(DbContextOptions<MyflixContext> options) : base(options)
		{
		}

        public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			OnModelCreatingPartial(modelBuilder);
		}
		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

	}
}
