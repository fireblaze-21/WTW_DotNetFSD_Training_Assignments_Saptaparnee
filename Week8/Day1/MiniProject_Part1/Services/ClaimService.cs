using MvcApplication.Models;
using MvcApplication.Repositories;

namespace MvcApplication.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _repo;

        public ClaimService(IClaimRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Claim> GetClaims()
        {
            return _repo.GetAll();
        }

        public Claim GetClaimById(int id)
        {
            return _repo.GetById(id);
        }

        public void CreateClaim(Claim claim)
        {
            claim.CreatedAt = DateTime.UtcNow;
            claim.UpdatedAt = DateTime.UtcNow;
            _repo.Add(claim);
            _repo.Save();
        }

        public void UpdateClaim(Claim claim)
        {
            claim.UpdatedAt = DateTime.UtcNow;
            _repo.Update(claim);
            _repo.Save();
        }

        public void DeleteClaim(int id)
        {
            var claim = _repo.GetById(id);
            if (claim != null)
            {
                _repo.Delete(claim);
                _repo.Save();
            }
        }
    }
}
