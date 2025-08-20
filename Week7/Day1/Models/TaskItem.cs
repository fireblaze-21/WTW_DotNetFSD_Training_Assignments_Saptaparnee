using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required]
        [Range(1, 5)]
        public int Priority { get; set; }
    }
}

