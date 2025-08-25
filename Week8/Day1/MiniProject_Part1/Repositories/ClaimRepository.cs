using MvcApplication.Data;
using MvcApplication.Models;

namespace MvcApplication.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly AppDbContext _context;

        public ClaimRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Claim> GetAll()
        {
            return _context.Claims.ToList();
        }

        public Claim GetById(int id)
        {
            return _context.Claims.Find(id);
        }

        public void Add(Claim claim)
        {
            _context.Claims.Add(claim);
        }

        public void Update(Claim claim)
        {
            _context.Claims.Update(claim);
        }

        public void Delete(Claim claim)
        {
            _context.Claims.Remove(claim);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
