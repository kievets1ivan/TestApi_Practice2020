using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.DTOs;

namespace TestApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<AuthResult> RegisterAsync(UserDTO userDTO);
    }
}