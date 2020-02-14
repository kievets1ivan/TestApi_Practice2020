using System.ComponentModel.DataAnnotations;

namespace TestApi.DAL.Entities
{
    public class UserAuth
    {
        [Required(ErrorMessage = "Fill in field 'Login' to sign in", AllowEmptyStrings = false)]
        [StringLength(255, ErrorMessage = "Are u sure?")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Fill in field 'Password' to sign in", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Are u sure?")]
        public string Password { get; set; }
    }
}
