using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projector.Models.Helpers;

namespace Projector.Models.InputModels
{
    public class CreateOrEditProjectInputModel
    {
        [Required(ErrorMessage = "Code is required."), StringLength(50, ErrorMessage = "Invalid code length.")]
        public string? Code { get; set; }
        [Required(ErrorMessage = "Name is required."), StringLength(50, ErrorMessage = "Invalid name length.")]
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        [Required, Precision(18, 4)]
        [Range(0, 99999999999999.9999, ErrorMessage = "Budget must be a positive number with up to 18 digits and 4 decimal places")]
        [RegularExpression(@"^\d{1,18}(\.\d{0,4})?$", ErrorMessage = "Budget must be up to 18 digits and 4 decimal places only")]

        public decimal Budget { get; set; }
        public string? SelectedCurrency { get; set; }
        public Dictionary<string, string> AvailableCurrencies => CurrencyHelper.Currencies;
        public IEnumerable<SelectListItem> CurrencyOptions =>
        AvailableCurrencies.Select(c => new SelectListItem
        {
            Value = c.Key,
            Text = c.Value
        });

    }
}
