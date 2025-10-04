using System;
using System.ComponentModel.DataAnnotations;

namespace HeberConnect.Models
{
    public class ODRequest
    {
        public int Id { get; set; }

        [Required]
        public string StudentId { get; set; }   // Who requested

        [Required]
        public DateTime Date { get; set; }

        // Either "FullDay" or "Hour1–5"
        [Required]
        public string RequestType { get; set; }   // "FullDay" or "Hour"

        public int? Hour { get; set; }   // Nullable if FullDay chosen

        [Required]
        [StringLength(200)]
        public string Reason { get; set; }

        public string Status { get; set; } = "Pending"; // Pending/Approved/Rejected
    }
}
