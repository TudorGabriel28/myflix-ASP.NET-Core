using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Models.Entities;
using DataAccess.Models.Parameters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class RepositoryMovie : Repository<Movie>, IRepositoryMovie
	{
		public RepositoryMovie(MyflixContext context) : base(context) { }

		public async Task<PagedList<Movie>> GetAllWithDetailsAsync(MovieParameters movieParameters)
		{
			var movies = _context.Movies
				.Include(m => m.Episodes)
				.Include(m => m.Genres)
				.Include(m => m.PrimaryImage)
				.Include(m => m.MeterRanking)
				.OrderBy(m => m.Title);

			return await PagedList<Movie>.ToPagedListAsync(movies, movieParameters.PageNumber, movieParameters.PageSize);
		}

		public async Task<Movie?> GetByImdbIdWithDetailsAsync(string imdbId)
        {
			var movie = await _context.Movies
				.Include(m => m.Episodes)
				.Include(m => m.Genres)
				.Include(m => m.PrimaryImage)
				.Include(m => m.MeterRanking)
				.SingleOrDefaultAsync(x => x.ImdbId == imdbId);
			return movie;
		}

		public async Task<Movie> GetByIdWithDetailsAsync(int id)
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
