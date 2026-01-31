using System.ComponentModel.DataAnnotations;
using EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName;

namespace MOM_PROJECT.Models;

public class DepartmentModel
{
    public int DepartmentID {get; set;}
    
    [Required(ErrorMessage = "Name is required")]
    public string DepartmentName  {get; set;}
    
    public DateTime Created {get; set;}
    
    public DateTime Modified {get; set;}
}