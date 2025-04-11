namespace Projector.Models.OutputModels
{
    public class ProjectDetailsDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }

        public decimal Budget { get; set; }
        public string Currency { get; set; }

        public List<PersonDTO> Members { get; set; } = [];
    }
}
