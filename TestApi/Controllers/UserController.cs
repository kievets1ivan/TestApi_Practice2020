using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApi.BL.DTOs;
using TestApi.BL.Services.Interfaces;
using TestApi.DAL.Entities;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    

        [HttpPost("/register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO newUser)
        {

            var authResponse = await _userService.RegisterAsync(newUser);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse);
            }

            return Ok();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> SignIn([FromBody] UserAuth user)
        {

            var authResponse = await _userService.SignInAsync(user);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }
    }
}
