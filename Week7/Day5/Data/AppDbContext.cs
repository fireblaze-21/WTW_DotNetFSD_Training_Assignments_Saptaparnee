using Microsoft.EntityFrameworkCore;
using MvcApplication.Models;

namespace MvcApplication.Data
{
    
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<Claim> Claims { get; set; }
        }
}
