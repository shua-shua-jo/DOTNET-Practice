using Projector.Models.OutputModels;
using Projector.Models.Entities;

namespace Projector.Models.ViewModels
{
    public class AssignmentViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<PersonDTO> NotMembers { get; set; }
        public List<PersonDTO> CurrMembers { get; set; }
        public bool HasCurrMembers => CurrMembers.Any();

        private AssignmentViewModel(Project project, List<PersonDTO> notMembers)
        {
            ProjectId = project.Id;
            ProjectName = project.Name;
            NotMembers = notMembers;
            CurrMembers = project.Persons.Select(p => new PersonDTO 
            { 
                Id = p.Id, 
                FirstName = p.FirstName, 
                LastName = p.LastName, 
                Username = p.UserName 
            }).ToList();
        }

        public static AssignmentViewModel? FromProject(Project project, List<PersonDTO> notMembers)
        {
            if (project == null) return null;
            return new AssignmentViewModel(project, notMembers ?? new List<PersonDTO>());
        }
    }
}
