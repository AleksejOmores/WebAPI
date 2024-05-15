using System.ComponentModel.DataAnnotations;

namespace PZ_webAPI.Models
{
    public class OrdersDTO
    {
        [Required]
        public Users UserID { get; set; }
        [Required]
        public DateOnly OrderDate { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
    }
}
