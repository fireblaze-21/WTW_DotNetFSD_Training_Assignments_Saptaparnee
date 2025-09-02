using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using InsuranceClaimSystem.Models;
using InsuranceClaimSystem.Services;
using Microsoft.AspNetCore.Authorization;

namespace InsuranceClaimSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires user to be logged in
    public class ClaimController : ControllerBase
    {
        private readonly ClaimService _claimService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClaimController(ClaimService claimService, UserManager<ApplicationUser> userManager)
        {
            _claimService = claimService;
            _userManager = userManager;
        }

        // POST: /api/claim
        [HttpPost]
        [Authorize(Roles = "Agent")] // Only agents can create claims
        public async Task<IActionResult> SubmitClaim([FromBody] ClaimRequest claim)
        {
            var user = await _userManager.GetUserAsync(User);
            claim.AgentId = user.Id;

            var result = await _claimService.CreateClaimAsync(claim);
            return Ok(result);
        }

        // GET: /api/claim
        [HttpGet]
        [Authorize(Roles = "Agent,Admin")] // Both agents and admins can view
        public async Task<IActionResult> GetClaims()
        {
            var user = await _userManager.GetUserAsync(User);
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var claims = await _claimService.GetClaimsAsync(user.Id, isAdmin);
            return Ok(claims);
        }

        // GET: /api/claim/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Agent,Admin")] // Same for single claim view
        public async Task<IActionResult> GetClaim(int id)
        {
            var claim = await _claimService.GetClaimByIdAsync(id);
            if (claim == null)
                return NotFound();

            return Ok(claim);
        }

        // PUT: /api/claim/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Agent,Admin")] // Only agent (own claim) or admin can update
        public async Task<IActionResult> UpdateClaim(int id, [FromBody] ClaimRequest updatedClaim)
        {
            var user = await _userManager.GetUserAsync(User);
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            updatedClaim.ClaimId = id;

            var success = await _claimService.UpdateClaimAsync(updatedClaim, user, isAdmin);
            if (!success)
                return Forbid();

            return Ok(new { message = "Claim updated successfully" });
        }

        // DELETE: /api/claim/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Agent,Admin")] // Only agent (own claim) or admin can delete
        public async Task<IActionResult> DeleteClaim(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var success = await _claimService.DeleteClaimAsync(id, user, isAdmin);
            if (!success)
                return Forbid();

            return Ok(new { message = "Claim deleted successfully" });
        }
    }
}

