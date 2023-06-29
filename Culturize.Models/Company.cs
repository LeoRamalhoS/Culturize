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

        public bool Active { get; set; } = true;
    }
}
