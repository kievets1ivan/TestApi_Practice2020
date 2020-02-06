using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestApi.DTOs;
using TestApi.Entities;
using TestApi.Options;

namespace TestApi.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<UserEntity> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;


        public UserService(UserManager<UserEntity> userManager,
            IMapper mapper,
            JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync() => _mapper.Map<IEnumerable<UserDTO>>(await _userManager.Users.ToListAsync());

        public async Task<AuthResult> RegisterAsync(UserDTO userDTO)
        {
            var existingUser = _userManager.Users.Where(x => x.UserLogin == userDTO.Login);

            if (existingUser.Any())
            {
                //в кастомный результат поставить Sucsess - false + можно ерор дописать 
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "User with this login already exists"
                };
            }

            var newUser = _mapper.Map<UserEntity>(userDTO);

            newUser.UserName = newUser.UserLogin;

            var result = await _userManager.CreateAsync(newUser, userDTO.Password);

            if (!result.Succeeded)
            {
                //в кастомный результат поставить Sucsess - false + можно ерор дописать(smth went wrong) 
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "Smth went wrong"
                };
            }

            return GenerateJwtToken(newUser);
        }

        private AuthResult GenerateJwtToken(UserEntity user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserLogin),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),//lifetime
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            return new AuthResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
