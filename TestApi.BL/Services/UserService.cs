using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestApi.BL.DTOs;
using TestApi.BL.Models;
using TestApi.BL.Services.Interfaces;
using TestApi.DAL.Entities;
using TestApi.DAL.Storages.Interfaces;

namespace TestApi.BL.Services
{
    public class UserService : IUserService
    {

        //private readonly UserManager<UserEntity> _userManager;
        private readonly IUserStorage _userStorage;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;


        public UserService(IUserStorage userStorage,
                            IMapper mapper,
                            JwtSettings jwtSettings)
        {
            _userStorage = userStorage;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthResult> SignInAsync(UserAuth user)
        {

            var userForSignIn = await _userStorage.FindByName(user.Login);

            if (userForSignIn != null)
            {
                if (await _userStorage.CheckPassword(userForSignIn, user.Password))
                {
                    return GenerateJwtToken(userForSignIn);
                }

                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "Your password is invalid"
                };
            }

            return new AuthResult
            {
                Success = false,
                ErrorMessage = "User with this login dosen't exist"
            };
        }


        public async Task<AuthResult> RegisterAsync(UserDTO userDTO)
        {
            //var existingUser = _userManager.Users.Where(x => x.UserLogin == userDTO.Login);
            var existingUser = await _userStorage.FindByName(userDTO.Login);


            if (existingUser != null)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "User with this login already exists"
                };
            }

            var newUser = _mapper.Map<UserEntity>(userDTO);

            var createdUser = await _userStorage.Create(newUser, userDTO.Password);

            if (!createdUser.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "Smth went wrong"
                };
            }

            return new AuthResult
            {
                Success = true,
            };
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
                Expires = DateTime.UtcNow.AddMinutes(1),//lifetime
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
