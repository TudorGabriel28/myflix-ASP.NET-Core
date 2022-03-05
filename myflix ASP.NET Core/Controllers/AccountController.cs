using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Models.Accounts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace myflix_ASP.NET_Core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            await _service.Register(model, Request.Headers["origin"]);
            return Ok(new { message = "Registration successful, please check your email for verification instructions" });
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
        {
            var response = await _service.Authenticate(model, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        // GET: <UserController>
        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        // GET <UserController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountResponse>> GetById(int id)
        {
            // users can get their own account and admins can get any account
            if (id != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var account = await _service.GetById(id);

            if (account != null)
            {
                return Ok(account);
            }

            return NotFound();
        }

        // POST <UserController>
        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<AccountResponse>>> Create(CreateRequest model)
        {
            var account = await _service.Create(model);

            if (account != null)
            {
                return Created($"User/{account.Id}", account);
            }

            return BadRequest();
        }
        /* public void Post([FromBody] string value)
         {
         }*/

        // PUT <UserController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<AccountResponse>> Update(int id, UpdateRequest model)
        {
            // users can update their own account and admins can update any account
            if (id != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            // only admins can update role
            if (Account.Role != Role.Admin)
                model.Role = "User";

            var account = await _service.Update(id, model);

            if(account != null)
            {
                return Ok(account);
            }

            return NotFound();
        }
        /*public void Put(int id, [FromBody] string value)
        {
        }*/

        // DELETE <UserController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // users can delete their own account and admins can delete any account
            if (id != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            bool result = await _service.Delete(id);

            if(result)
            {
                return Ok(new { message = "Account deleted successfully" });
            }

            return NotFound();
        }

        // helper methods

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}

