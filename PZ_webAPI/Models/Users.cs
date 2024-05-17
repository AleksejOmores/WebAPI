using System.ComponentModel.DataAnnotations;

namespace PZ_webAPI.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; } 
    }
}
