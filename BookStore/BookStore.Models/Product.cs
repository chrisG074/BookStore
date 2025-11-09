using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Omschrijving")]
        public string? Description { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{10}|[0-9]{13})$", ErrorMessage = "ISBN moet 10 or 13 cijfers zijn.")]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Display(Name = "Schrijver")]
        public string? Author { get; set; }

        [Required]
        [Display(Name = "Catalogusprijs")]
        [Range(1, 10000)]
        public decimal ListPrice { get; set; }

        [Required]
        [Display(Name = "Prijs")]
        [Range(1, 10000)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Prijs bij 50+ afname")]
        [Range(1, 10000)]
        public decimal Price50 { get; set; }

        [Required]
        [Display(Name = "Prijs bij 100+ afname")]
        [Range(1, 10000)]

        public decimal Price100 { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        [Display(Name = "Categorie")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        [Display(Name = "Soort kaft")]
        public int CoverTypeId { get; set; }
        public CoverType? CoverType { get; set; }
    }
}