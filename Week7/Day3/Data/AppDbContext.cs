using Microsoft.EntityFrameworkCore;
using System.Numerics;
using WebApplication7.Models;

namespace WebApplication7.Data
{
    
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<Patient> Patients { get; set; }
            public DbSet<Doctor> Doctors { get; set; }
            public DbSet<Appointment> Appointments { get; set; }
        }
    }

