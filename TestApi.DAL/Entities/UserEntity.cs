using Microsoft.AspNetCore.Identity;

namespace TestApi.DAL.Entities
{
    public class UserEntity : IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserLogin { get; set; }
    }
}
