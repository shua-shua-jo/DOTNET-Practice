using Projector.Models.OutputModels;

namespace Projector.Models.ViewModels
{
    public class AssignmentViewModel
    {
        public int ProjectId {  get; set; }
        public string ProjectName { get; set; }
        public List<PersonDTO> NotMembers { get; set; }
        public List<PersonDTO> CurrMembers { get; set; }
        public bool HasCurrMembers => CurrMembers.Any();
    }
}
