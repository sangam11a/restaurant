using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Models
{
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoginId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(100,MinimumLength=5)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters, include uppercase, lowercase, number, and special character.")]
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }

        public bool Verified { get; set; } = false;
        public string VerificationCode { get; set; }= String.Empty;
        //[ForeignKey("EmpId")]
        //public Guid EmpId { get; set; }

        //public Employee Employee { get; set; }
    }
}
