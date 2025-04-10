using System.ComponentModel.DataAnnotations;

namespace Projector.Models.InputModels
{
    public class SignInInputModel
    {
        [Required(ErrorMessage = "Username is required."), EmailAddress(ErrorMessage = "Invalid username."), MinLength(5), MaxLength(200)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Email address must be 5 to 200 characters long.")]
        public required string Username { get; set; }
        [Required(ErrorMessage = "Password is required."), MinLength(7), MaxLength(11)]
        [DataType(DataType.Password, ErrorMessage = "Invalid password.")]
        [StringLength(11, MinimumLength = 7, ErrorMessage = "Password must be 7 to 11 characters long.")]
        public required string Password { get; set; }
    }
}
