using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Culturize.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FName { get; set; }
        public string? MName { get; set; }
        public string? LName { get; set; }

        public int? CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        [ValidateNever]
        public Company? Company { get; set; }
    }
}