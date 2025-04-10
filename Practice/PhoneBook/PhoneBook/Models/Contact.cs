using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string? Company { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; } = new();
    }
}