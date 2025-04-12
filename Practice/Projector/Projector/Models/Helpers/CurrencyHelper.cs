using System.Globalization;

namespace Projector.Models.Helpers
{
    public class CurrencyHelper
    {
        public static readonly Dictionary<string, string> Currencies = new Dictionary<string, string>
        {
            {"USD", "US Dollar ($)"},
            {"EUR", "Euro (€)"},
            {"PHP", "Philippine Peso (₱)"},
            // Add more as needed
        };
        public static string FormatCurrency(decimal amount, string? currencyCode)
        {
            var culture = currencyCode switch
            {
                "USD" => new CultureInfo("en-US"),
                "EUR" => new CultureInfo("de-DE"),
                "PHP" => new CultureInfo("en-PH"),
                _ => CultureInfo.CurrentCulture
            };

            return amount.ToString("C2", culture);
        }
    }
}
