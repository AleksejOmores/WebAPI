using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PZ_webAPI.Models
{
    public class UsersDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
