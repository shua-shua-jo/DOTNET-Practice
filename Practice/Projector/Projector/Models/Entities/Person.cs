using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projector.Models.Entities
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        [RegularExpression("[A-Za-z]+")]
        public required string LastName { get; set; }
        [Required, MaxLength(50)]
        public required string FirstName { get; set; }
        [Required, EmailAddress, MinLength(5), MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        public required string UserName { get; set; }
        [Required, MinLength(7), MaxLength(11)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        
        public List<Project> Projects { get; } = [];
    }
}
