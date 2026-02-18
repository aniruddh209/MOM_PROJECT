using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MOM_PROJECT.Models
{
    public class StaffModel
    {
        public int StaffID { get; set; }

        // For LIST page only
        public string? DepartmentName { get; set; }

        // REQUIRED for INSERT / UPDATE
        [Required(ErrorMessage = "Department is required")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Staff Name is required")]
        [StringLength(100)]
        public string StaffName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$")]
        public string MobileNo { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public string? Remarks { get; set; }

        // ✅ ONLY ADDITION (for Department dropdown)
        public List<SelectListItem>? DepartmentList { get; set; }
    }
}