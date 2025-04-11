using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Projector.Models.Entities
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is required."), StringLength(50, ErrorMessage = "Invalid code length.")]
        public required string Code { get; set; }
        [Required(ErrorMessage = "Name is required."), StringLength(50, ErrorMessage = "Invalid name length.")]
        public required string Name { get; set; }
        [AllowNull]
        public string Remarks { get; set; }
        [Required, Precision(18, 4)]
        [RegularExpression(@"^\d{1,18}(\.\d{0,4})?$", ErrorMessage = "Value must have up to 18 digits and up to 4 decimal places")]
        [Range(0, 99999999999999.9999, ErrorMessage = "Value must be a positive number with up to 18 digits and 4 decimal places")]

        public required decimal Budget { get; set; }
        [Required]
        public required string Currency {  get; set; }
        public List<Person> Persons { get; } = [];
    }
}
