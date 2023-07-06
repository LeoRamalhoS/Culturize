using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }

        public bool Active { get; set; } = true;
        public string? Address { get; set; }
        public string? AddressNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? Phone { get; set; }
        public string? LogoBlobFile { get; set; }

        [NotMapped]
        [ValidateNever]
        public string LogoUrl { get; set; }
    }
}
