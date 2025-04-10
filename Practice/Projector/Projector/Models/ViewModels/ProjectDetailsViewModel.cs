

namespace Projector.Models.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }

        public decimal Budget { get; set;  }
        public string FormattedBudget => Budget.ToString("C4");
        public List<ProjectMembersViewModel> Members { get; set; } = [];

        public bool HaveMembers => Members.Any();
    }
}
