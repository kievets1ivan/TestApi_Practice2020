using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Entities
{
    public class UserEntity : IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserLogin { get; set; }
    }
}
