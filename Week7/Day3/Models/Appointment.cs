using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Appointment Date & Time")]
        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(Appointment), nameof(ValidateFutureDate))]
        public DateTime AppointmentDateTime { get; set; }

        [Required]
        public string Status { get; set; } // Scheduled, Completed, Cancelled

        // Foreign Keys
        [Required]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        [Required]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        // Custom validation for appointment datetime
        public static ValidationResult? ValidateFutureDate(DateTime date, ValidationContext context)
        {
            if (date < DateTime.Now)
            {
                return new ValidationResult("Appointment must be scheduled for a future date/time.");
            }
            return ValidationResult.Success;
        }
    }
}
