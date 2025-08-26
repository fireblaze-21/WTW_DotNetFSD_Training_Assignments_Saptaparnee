using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; // Add this using directive

namespace WebApplication10.Model
{
    public class Dept
    {
        [Key]
        public int Deptno { get; set; }

        [Required]
        public string Dname { get; set; }

        public string Loc { get; set; }

        [JsonIgnore]
        // Navigation property (one-to-many)
        public ICollection<Emp>? Emps { get; set; }
    }
}
