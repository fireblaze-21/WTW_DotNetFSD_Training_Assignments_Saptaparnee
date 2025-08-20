using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Specialty { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
