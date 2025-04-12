using System.Globalization;
using Projector.Models.Helpers;
using Projector.Models.OutputModels;

namespace Projector.Models.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public decimal Budget { get; set; }
        public string Currency { get; set; }
        public string FormattedBudget => CurrencyHelper.FormatCurrency(Budget, Currency);
        public List<ProjectMembersViewModel> Members { get; set; } = [];
        public bool HaveMembers => Members.Any();

        private ProjectDetailsViewModel(ProjectDetailsDTO dto)
        {
            Code = dto.Code!;
            Name = dto.Name!;
            Budget = dto.Budget;
            Remarks = dto.Remarks!;
            Currency = dto.Currency!;
            Members = dto.Members.Select(m => new ProjectMembersViewModel(m)).ToList();
        }

        public static ProjectDetailsViewModel? FromDTO(ProjectDetailsDTO dto)
        {
            if (dto == null) return null;
            return new ProjectDetailsViewModel(dto);
        }
    }
}
