using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [CustomValidation(typeof(Patient), nameof(ValidateDOB))]
        public DateTime DateOfBirth { get; set; } 

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }

        // Custom DOB validation
        public static ValidationResult? ValidateDOB(DateTime dob, ValidationContext context)
        {
            if (dob > DateTime.Now)
            {
                return new ValidationResult("Date of Birth cannot be in the future.");
            }

            int age = DateTime.Now.Year - dob.Year;
            if (dob > DateTime.Now.AddYears(-age)) age--;

            if (age > 120)
            {
                return new ValidationResult("Age cannot be greater than 120 years.");
            }

            return ValidationResult.Success;
        }
    }
}
