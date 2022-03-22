using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace myflix_ASP.NET_Core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WishListController : BaseController
    {
        private readonly IWishListService _service;
        public WishListController(IWishListService service)
        {
            _service = service;
        }
        // GET: <WishListController>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> Get()
        {
            var result = await _service.Get(Account.Id);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest();
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
            return BadRequest(new { message = "Movie can't be added to wishlist" });
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
            return BadRequest(new { message = "Movie can't be removed from wishlist" });
        }
    }
}
