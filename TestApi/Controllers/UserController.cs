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
    

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAllAsync());
        }

        // GET: api/User/5
        /*[HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int userId)
        {
            return Ok();
        }*/

        // POST: api/User
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

        


        // PUT: api/User/5
       /* [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int userId, [FromBody] UserEntity user)
        {

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int userId)
        {
            return Ok();
        }*/

        [HttpPost("/login")]
        public IActionResult AuthUser([FromBody] UserAuth user)//возвращаем токен
        {
            return Ok();
            //проверка логина и пароля в базе данных

            //если юзер существует, то генерим и возвращаем токен
            //если нет, то просим зарегестрироваться
        }
    }
}
