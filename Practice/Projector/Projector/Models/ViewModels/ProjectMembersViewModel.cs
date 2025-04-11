namespace Projector.Models.ViewModels
{
    public class ProjectMembersViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
