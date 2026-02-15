using System.ComponentModel.DataAnnotations;

namespace MOM_PROJECT.Models
{
    public class StaffModel
    {
        [Required(ErrorMessage = "Staff Name is required")]
        [StringLength(100, ErrorMessage = "Staff Name cannot exceed 100 characters")]
        public string StaffName { get; set; }

        public int StaffID { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Department")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Mobile Number must be 10 digits")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        [StringLength(250, ErrorMessage = "Remarks cannot exceed 250 characters")]
        public string? Remarks { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}