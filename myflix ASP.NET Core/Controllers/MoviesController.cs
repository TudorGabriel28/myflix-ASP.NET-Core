using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Models.Entities;
using DataAccess.Models.Parameters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace myflix_ASP.NET_Core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : BaseController
    {
        private readonly IMovieService _service;
        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        // GET: <MovieController>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAll([FromQuery] MovieParameters movieParameters)
        {
            var movies = await _service.GetAll(movieParameters);

            var metadata = new
            {
                movies.TotalCount,
                movies.PageSize,
                movies.CurrentPage,
                movies.TotalPages,
                movies.HasNext,
                movies.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(movies);
        }

        // GET <MoviesController>/5
        [Authorize]
        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieResponse>> Get(int movieId)
        {
            var movie = await _service.GetById(movieId, Account.Id);

            if (movie != null)
            {
                return Ok(movie);
            }
            return NotFound();
        }

        // POST: <MoviesController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Movie>> Create([FromBody] string imdbMovieId)
        {
            var movie = await _service.Create(imdbMovieId, Account.Id);
            if (movie != null)
            {
                return Created($"Movie/{movie.Id}", movie);
            }
            return NotFound();
        }

        // DELETE <MoviesController>/5
        [Authorize(Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _service.Delete(id);

            if (result)
            {
                return Ok(new { message = "Movie deleted successfully" });
            }

            return NotFound();
        }
    }
}
