using System;
using System.ComponentModel.DataAnnotations;

namespace HeberConnect.Models
{
    public class Complaint
    {
        public int Id { get; set; }

        [Required]
        public string StudentId { get; set; }   // Who submitted

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Category { get; set; }    // e.g., Electrical, Cleanliness, Staff, General

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public string Status { get; set; } = "Pending"; // Pending / Resolved / Rejected
    }
}
