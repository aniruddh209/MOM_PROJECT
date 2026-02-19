using System.ComponentModel.DataAnnotations;

namespace MOM_PROJECT.Models
{
    public class UserModel
    {
        [Key] // ✅ EXPLICIT PRIMARY KEY
        public int UserId { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }
    }
}