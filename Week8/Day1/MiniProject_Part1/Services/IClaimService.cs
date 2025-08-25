using MvcApplication.Models;

namespace MvcApplication.Services
{
    public interface IClaimService
    {
        IEnumerable<Claim> GetClaims();
        Claim GetClaimById(int id);
        void CreateClaim(Claim claim);
        void UpdateClaim(Claim claim);
        void DeleteClaim(int id);
    }
}

