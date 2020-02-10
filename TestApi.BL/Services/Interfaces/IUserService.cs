using System.Threading.Tasks;
using TestApi.BL.DTOs;
using TestApi.DAL.Entities;

namespace TestApi.BL.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthResult> RegisterAsync(UserDTO userDTO);
        Task<AuthResult> SignInAsync(UserAuth user);
    }
}
