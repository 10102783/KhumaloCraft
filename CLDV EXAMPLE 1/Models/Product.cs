using System.ComponentModel.DataAnnotations;

namespace KhumaloCraft.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string PicUrl { get; set; }
        [Required]
        public bool Availability { get; set; }
    }
}
