using Projector.Models.OutputModels;

namespace Projector.Models.ViewModels
{
    public class ProjectMembersViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public ProjectMembersViewModel(PersonDTO dto)
        {
            FirstName = dto.FirstName!;
            LastName = dto.LastName!;
        }
    }
}
