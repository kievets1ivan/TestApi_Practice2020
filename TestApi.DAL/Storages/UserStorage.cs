using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.DAL.Entities;
using TestApi.DAL.Storages.Interfaces;

namespace TestApi.DAL.Storages
{
    public class UserStorage : IUserStorage
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserStorage(UserManager<UserEntity> userManager,
                           IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetCurrentUserId()
        {
            return new Guid(_userManager.Users.SingleOrDefault(x => x.UserName == _userManager.GetUserId(_httpContextAccessor.HttpContext.User)).Id);
        }

        public UserEntity GetUserById(string userId)
        {
            return _userManager.Users.SingleOrDefault(x => x.Id == userId);
        }

        public async Task<UserEntity> FindByName(string login) => await _userManager.FindByNameAsync(login);

        public async Task<bool> CheckPassword(UserEntity user, string password) => await _userManager.CheckPasswordAsync(user, password);

        public async Task<IdentityResult> Create(UserEntity newUser, string password) => await _userManager.CreateAsync(newUser, password);

    }
}
