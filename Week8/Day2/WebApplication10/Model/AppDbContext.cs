using Microsoft.EntityFrameworkCore;

namespace WebApplication10.Model
{
    public class AppDbContext : DbContext
    {
        public DbSet<Emp> Emps { get; set; }
        public DbSet<Dept> Depts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-many relation
            modelBuilder.Entity<Dept>()
                .HasMany(d => d.Emps)
                .WithOne(e => e.Dept)
                .HasForeignKey(e => e.Deptno);
        }
    }
}
