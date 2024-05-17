using System.ComponentModel.DataAnnotations;

namespace PZ_webAPI.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int Price { get; set; }
    }
}
