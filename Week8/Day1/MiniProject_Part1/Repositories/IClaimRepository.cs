using MvcApplication.Models;

namespace MvcApplication.Repositories
{
    public interface IClaimRepository
    {
        IEnumerable<Claim> GetAll();
        Claim GetById(int id);
        void Add(Claim claim);
        void Update(Claim claim);
        void Delete(Claim claim);
        void Save();
    }
}
