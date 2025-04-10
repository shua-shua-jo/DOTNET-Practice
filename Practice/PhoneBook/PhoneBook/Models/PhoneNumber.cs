using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }

        [Required]
        [StringLength(20)]
        public string Label { get; set; } // Or use PhoneNumberType enum

        public int ContactId { get; set; }

        [ForeignKey(nameof(ContactId))]
        public Contact Contact { get; set; }
    }
}