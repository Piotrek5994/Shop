using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Product
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Plis enter a value")]
        public string Name { get; set; }

        public string Slug { get; set; }
        [Required, MinLength(4, ErrorMessage = "Minmal Lenght is 2 ,plis enter a value")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Required,Range(1, int.MaxValue, ErrorMessage = "You must choose a Category")]
        public long CategoryId { get; set; }

        public Category Category { get; set; }
        public string Image { get; set; } = "noimage.png";

        [NotMapped]
        [FileExtensions]
        public IFormFile ImageUpload { get; set; }
    }
}

