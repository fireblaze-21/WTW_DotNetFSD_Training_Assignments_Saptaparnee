using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InsuranceClaimSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public string Role { get; set; } = "Agent";
        [Required]
        public string Role { get; set; } = "Agent"; // default
    }
}