using System.Threading.Tasks;
using TestApi.BL.DTOs;
using TestApi.DAL.Entities;

namespace TestApi.BL.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthResult> Register(UserDTO userDTO);
        Task<AuthResult> SignIn(UserAuth user);
    }
}
