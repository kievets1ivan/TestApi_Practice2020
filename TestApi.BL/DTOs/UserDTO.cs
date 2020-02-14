using System.ComponentModel.DataAnnotations;

namespace TestApi.BL.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Fill in field 'First Name', please", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "First Name can't be over 50 symbols")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Fill in field 'Last Name', please", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Last Name can't be over 50 symbols")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Fill in field 'Login', please", AllowEmptyStrings = false)]
        [StringLength(255, ErrorMessage = "Login can't be over 255 symbols")]
        [DataType(DataType.EmailAddress)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Fill in field 'Password', please", AllowEmptyStrings = false)]
        [StringLength(255, ErrorMessage = "Password can't be over 255 symbols and less 6 symbols", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
