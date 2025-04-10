namespace Projector.Models.OutputModels
{
    public class ProjectItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Budget { get; set; }
        // never use this formatting
        public string FormattedBudget => Budget.ToString("C4");
    }
}
