using System;
using System.ComponentModel.DataAnnotations;

namespace MOM_PROJECT.Models
{
    public class MeetingModel
    {
        [Key]
        public int MeetingID { get; set; }
        
        [Required(ErrorMessage = "Meeting date is required")]
        public DateTime? MeetingDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select meeting venue")]
        public int MeetingVenueID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select meeting type")]
        public int MeetingTypeID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select department")]
        public int DepartmentID { get; set; }

        public string? MeetingDescription { get; set; }
        public string? DocumentPath { get; set; }

        // ===== AUDIT =====
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Modified { get; set; } = DateTime.Now;

        // ===== CANCELLATION =====
        [Required(ErrorMessage = "Please select cancellation status")]
        public bool? IsCancelled { get; set; }

        public DateTime? CancellationDateTime { get; set; }
        public string? CancellationReason { get; set; }
    }
}