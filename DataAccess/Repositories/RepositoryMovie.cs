using DataAccess.Models;
using DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class RepositoryMovie : Repository<Movie>, IRepositoryMovie
	{
		public RepositoryMovie(MyflixContext context) : base(context) { }
		
		public async Task<Movie?> GetByImdbIdAsync(string imdbId)
        {
			var movie = await _context.Movies
				.Include(m => m.Episodes)
				.Include(m => m.Genres)
				.Include(m => m.PrimaryImage)
				.Include(m => m.MeterRanking)
				.SingleOrDefaultAsync(x => x.ImdbId == imdbId);
			return movie;
		}

		public override async Task<IEnumerable<Movie>> GetAllAsync()
        {
			var movies = await _context.Movies
				.Include(m => m.Episodes)
				.Include(m => m.Genres)
				.Include(m => m.PrimaryImage)
				.Include(m => m.MeterRanking).ToListAsync();

			return movies;
		}

		public new async Task<Movie> GetByIdAsync(int id, bool asNoTracking = false)
		{
			var movie = await _context.Movies.Where(m => m.Id == id)
				.Include(m => m.Episodes)
				.Include(m => m.Genres)
				.Include(m => m.PrimaryImage)
				.Include(m => m.MeterRanking)
				.SingleOrDefaultAsync();
			return movie;
		}
	}
}
