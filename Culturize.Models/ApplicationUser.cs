using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Culturize.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FName { get; set; }
        public string? MName { get; set; }
        public string? LName { get; set; }

    }
}