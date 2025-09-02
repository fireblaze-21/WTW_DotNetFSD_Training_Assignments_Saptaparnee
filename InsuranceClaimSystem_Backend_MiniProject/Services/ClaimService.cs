using InsuranceClaimSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimSystem.Services
{
    public class ClaimService
    {
        private readonly ApplicationDbContext _context;

        public ClaimService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClaimRequest>> GetClaimsAsync(string userId, bool isAdmin)
        {
            if (isAdmin)
            {
                return await _context.Claims.Include(c => c.Agent).ToListAsync();
            }

            return await _context.Claims
                .Where(c => c.AgentId == userId)
                .Include(c => c.Agent)
                .ToListAsync();
        }

        public async Task<ClaimRequest> GetClaimByIdAsync(int id)
        {
            return await _context.Claims.Include(c => c.Agent).FirstOrDefaultAsync(c => c.ClaimId == id);
        }

        public async Task<ClaimRequest> CreateClaimAsync(ClaimRequest claim)
        {
            claim.CreatedAt = DateTime.UtcNow;
            claim.UpdatedAt = DateTime.UtcNow;

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();
            return claim;
        }

        public async Task<bool> UpdateClaimAsync(ClaimRequest updatedClaim, ApplicationUser user, bool isAdmin)
        {
            var claim = await _context.Claims.FirstOrDefaultAsync(c => c.ClaimId == updatedClaim.ClaimId);
            if (claim == null) return false;

            if (isAdmin)
            {
                claim.Status = updatedClaim.Status;
            }
            else if (claim.AgentId == user.Id && claim.Status == "New")
            {
                claim.Description = updatedClaim.Description;
                claim.ClaimAmount = updatedClaim.ClaimAmount;
            }
            else
            {
                return false; // Not authorized to update
            }

            claim.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteClaimAsync(int id, ApplicationUser user, bool isAdmin)
        {
            var claim = await _context.Claims.FirstOrDefaultAsync(c => c.ClaimId == id);
            if (claim == null) return false;

            if (isAdmin || (claim.AgentId == user.Id && claim.Status == "New"))
            {
                _context.Claims.Remove(claim);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
