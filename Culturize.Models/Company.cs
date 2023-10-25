using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Culturize.Models
{
    public class Company
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey(nameof(Company.ParentId))]
        [ValidateNever]
        public Company? ParentCompany { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;

        public bool Active { get; set; } = true;
        public string? Address { get; set; }
        public string? AddressNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? Phone { get; set; }
        [DisplayName("Logo")]
        public string? LogoBlobFile { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? DeactivatedAt { get; set; }

        [NotMapped]
        [ValidateNever]
        public string LogoUrl { get; set; }
    }
}
