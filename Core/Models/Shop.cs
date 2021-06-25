using System.ComponentModel.DataAnnotations;

namespace Domain.Core.Models
{
    public class Shop
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
