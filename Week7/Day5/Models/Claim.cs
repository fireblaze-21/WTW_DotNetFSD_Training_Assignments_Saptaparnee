using System.ComponentModel.DataAnnotations;

namespace MvcApplication.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }

        [Required]
        public string PolicyNumber { get; set; }

        [Required]
        public string ClaimType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, 1000000)]
        public decimal ClaimAmount { get; set; }

        [Required]
        public string Status { get; set; } = "New";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // NOTE: AgentId is string since AspNetUsers.Id is string
        public string AgentId { get; set; }
    }
}
