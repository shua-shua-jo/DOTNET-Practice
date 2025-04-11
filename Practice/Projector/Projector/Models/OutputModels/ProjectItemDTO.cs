using Projector.Models.Helpers;

namespace Projector.Models.OutputModels
{
    public class ProjectItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Budget { get; set; }
        public string Currency { get; set; }
        public string FormattedBudget => CurrencyHelper.FormatCurrency(Budget, Currency);
    }
}
