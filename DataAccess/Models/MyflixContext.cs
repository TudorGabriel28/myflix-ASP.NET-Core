using DataAccess.Models.Entities;
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

        public DbSet<Account> Accounts { get; set; }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Episodes> Episodes { get; set; }
        public DbSet<PrimaryImage> PrimaryImages { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MeterRanking> MeterRankings { get; set; }



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
