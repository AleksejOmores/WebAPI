using System.ComponentModel.DataAnnotations;

namespace PZ_webAPI.Models
{
    public class ProductDTO
    {
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
