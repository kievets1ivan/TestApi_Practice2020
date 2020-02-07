using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.DTOs;
using TestApi.Entities;

namespace TestApi.Services
{
    public interface IUserService
    {
        Task<AuthResult> RegisterAsync(UserDTO userDTO);
        Task<AuthResult> SignInAsync(UserAuth user);
    }
}