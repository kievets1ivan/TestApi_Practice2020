using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestApi.DataLayer;
using TestApi.DTOs;
using TestApi.Entities;
using TestApi.Services;

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
                return BadRequest(authResponse.ErrorMessage);
            }

            return Ok(authResponse.Token);
        }

        [HttpPost("/login")]
        public async Task<IActionResult> SignIn([FromBody] UserAuth user)
        {

            var authResponse = await _userService.SignInAsync(user);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse.ErrorMessage);
            }

            return Ok(authResponse.Token);
        }
    }
}
