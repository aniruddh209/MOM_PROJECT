using System.ComponentModel.DataAnnotations;

namespace MOM_PROJECT.Models;

public class MeetingMemberModel
{
    public int MeetingMemberID { get; set; }

    [Required]
    public int MeetingID { get; set; }

    [Required]
    public int StaffID { get; set; }

    [Required(ErrorMessage = "Please select presence status")]
    public bool? IsPresent { get; set; }
    
    public string Remarks { get; set; } = string.Empty;

    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}