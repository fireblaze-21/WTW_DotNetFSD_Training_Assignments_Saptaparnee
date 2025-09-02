using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaimSystem.Models
{
    public class ClaimRequest
    {
        [Key]
        public int ClaimId { get; set; }

        [Required]
        public string PolicyNumber { get; set; }

        [Required]
        public string? ClaimType { get; set; } // Health, Auto, etc.

        
        public string? Description { get; set; }

        [Range(0, 1000000)]
        public decimal ClaimAmount { get; set; }

        public string Status { get; set; } = "New"; // New, In Review, Approved, Rejected

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key
        [Required]
        public string? AgentId { get; set; }

        [ForeignKey("AgentId")]
        public ApplicationUser? Agent { get; set; }
    }
}

