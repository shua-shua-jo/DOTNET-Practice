using System.ComponentModel.DataAnnotations;

namespace Projector.Models.InputModels
{
    public class CreateNewPersonInputModel
    {
        [Required(ErrorMessage = "Last name is required."), MaxLength(50, ErrorMessage = "Name length must be less than 50 characters.")]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Invalid name value.")]
        public required string LastName { get; set; }
        [Required(ErrorMessage = "First name is required."), MaxLength(50, ErrorMessage = "Name length must be less than 50 characters.")]
        [RegularExpression("[A-Za-z ]+",ErrorMessage = "Invalid name value.")]
        public required string FirstName { get; set; }
        [Required, EmailAddress(ErrorMessage = "Invalid email address."), MinLength(5), MaxLength(200)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Email address must be 5 to 200 characters long.")]
        public required string UserName { get; set; }
        [Required(ErrorMessage = "Password is required."), MinLength(7), MaxLength(11)]
        [DataType(DataType.Password, ErrorMessage = "Invalid password.")]
        [StringLength(11, MinimumLength = 7, ErrorMessage = "Password must be 7 to 11 characters long.")]
        public required string Password { get; set; }
    }
}
