using System.ComponentModel.DataAnnotations;

namespace MOM_PROJECT.Models
{
    public class StaffModel
    {
        public int StaffID { get; set; }

        [Required(ErrorMessage = "Staff name is required")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters allowed")]
        public string StaffName { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^[6-9]\d{9}$",
            ErrorMessage = "Enter valid 10-digit mobile number")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        public string Email { get; set; }

        [StringLength(200, ErrorMessage = "Maximum 200 characters allowed")]
        public string Remarks { get; set; }
    }
}