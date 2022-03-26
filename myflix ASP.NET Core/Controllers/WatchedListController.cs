using BusinessLogic.Interfaces;
using DataAccess.Models.Entities;
using DataAccess.Models.Parameters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace myflix_ASP.NET_Core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WatchedListController : BaseController
    {
        private readonly IWatchedListService _service;
        public WatchedListController(IWatchedListService service)
        {
            _service = service;
        }
        // GET: <WishListController>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> Get([FromQuery] MovieParameters movieParameters)
        {
            var watchedList = await _service.Get(movieParameters, Account.Id);

            var metadata = new
            {
                watchedList.TotalCount,
                watchedList.PageSize,
                watchedList.CurrentPage,
                watchedList.TotalPages,
                watchedList.HasNext,
                watchedList.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(watchedList);
        }

        // PUT <WishListController>/5
        [Authorize]
        [HttpPut("add/{MovieId}")]
        public async Task<ActionResult> AddMovie(int MovieId)
        {
            var result = await _service.Add(Account.Id, MovieId);
            if (result)
            {
                return Ok();
            }
            return BadRequest(new { message = "Movie can't be added to watchedlist" });
        }

        // PUT <WishListController>/5
        [Authorize]
        [HttpPut("remove/{MovieId}")]
        public async Task<ActionResult> RemoveMovie(int MovieId)
        {
            var result = await _service.Remove(Account.Id, MovieId);
            if (result)
            {
                return Ok();
            }
            return BadRequest(new { message = "Movie can't be removed from watchedlist" });
        }
    }
}
