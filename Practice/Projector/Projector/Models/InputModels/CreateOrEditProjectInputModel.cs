using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Projector.Models.InputModels
{
    public class CreateOrEditProjectInputModel
    {
        [Required(ErrorMessage = "Code is required."), StringLength(50, ErrorMessage = "Invalid code length.")]
        public required string Code { get; set; }
        [Required(ErrorMessage = "Name is required."), StringLength(50, ErrorMessage = "Invalid name length.")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Remarks is required.")]
        public required string Remarks { get; set; }
        [Required, Precision(18, 4)]
        [Range(0, 99999999999999.9999, ErrorMessage = "Budget must be a positive number with up to 18 digits and 4 decimal places")]
        [RegularExpression(@"^\d{1,18}(\.\d{0,4})?$", ErrorMessage = "Budget must be up to 18 digits and 4 decimal places only")]

        public required decimal Budget { get; set; }
    }
}
