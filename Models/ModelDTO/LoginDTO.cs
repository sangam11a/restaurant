using System.ComponentModel.DataAnnotations;

namespace Stock.Models.ModelDTO
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters, include uppercase, lowercase, number, and special character.")]
        public string Password { get; set; }

    }
}
