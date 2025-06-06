using Microsoft.AspNetCore.Mvc;
using Stock.Models;
using Stock.Models.ModelDTO;
using Microsoft.AspNetCore.Identity;
using Stock.Data;


namespace Stock.Controllers
{

    public class PasswordService
    {
        private readonly PasswordHasher<object> _hasher = new PasswordHasher<object>();

        // Hash the password for storing
        public string HashPassword(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        // Verify the password during login
        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public LoginController(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public IActionResult AddLogin(LoginDTO loginDto)
        {
            var service = new PasswordService();
            var hashedPassword = service.HashPassword(loginDto.Password);
            var newData = new Login { 
                Username = loginDto.Username, 
                Password = hashedPassword,
            };
            dbContext.Logins.Add(newData);
            dbContext.SaveChanges();
            return Ok(newData);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
