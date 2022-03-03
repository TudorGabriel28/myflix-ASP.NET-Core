using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace myflix_ASP.NET_Core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // GET: <UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        // GET <UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var result = await _service.GetById(id);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        // POST <UserController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> Add(User user)
        {
            /*if (!await _service.CheckUniqueName(user.Name))
            {
                return Conflict("Name already exists!");
            }*/

            var UserAdded = await _service.Add(user);

            if (UserAdded != null)
            {
                return Created($"User/{UserAdded.Id}", UserAdded);
            }

            return BadRequest();
        }
       /* public void Post([FromBody] string value)
        {
        }*/

        // PUT <UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(User user)
        {
            var UserUpdated = await _service.Update(user);

            if (UserUpdated != null)
            {
                return NoContent();
            }

            return NotFound();
        }
        /*public void Put(int id, [FromBody] string value)
        {
        }*/

        // DELETE <UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _service.Delete(id);

            if(result)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
