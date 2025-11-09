using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Soort kaft")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}