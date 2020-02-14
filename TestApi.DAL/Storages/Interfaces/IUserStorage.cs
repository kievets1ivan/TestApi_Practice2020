using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApi.DAL.Entities;

namespace TestApi.DAL.Storages.Interfaces
{
    public interface IUserStorage
    {
        Guid GetCurrentUserId();
        UserEntity GetUserById(string userId);
        Task<UserEntity> FindByName(string login);
        Task<bool> CheckPassword(UserEntity user, string password);
        Task<IdentityResult> Create(UserEntity newUser, string password);
    }
}
