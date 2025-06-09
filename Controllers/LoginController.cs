
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using Stock.Models;
//using Stock.Models.ModelDTO;
//using Stock.Services;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.AspNetCore.Authorization;
//using Stock.Data;
//using Stock.Services;

//namespace Stock.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LoginController : ControllerBase
//    {
//        private readonly IConfiguration _config;
//        private readonly ApplicationDbContext _db;

//        public LoginController(ApplicationDbContext db, IConfiguration config)
//        {
//            _db = db;
//            _config = config;
//        }

//        [HttpPost("register")]
//        public IActionResult Register(LoginDTO dto)
//        {
//            var service = new PasswordService();
//            var hashed = service.HashPassword(dto.Password).ToString();

//            var user = new Login
//            {
//                Username = dto.Username,
//                Password = hashed,
//                AccessToken=String.Empty
//            };

//            _db.Logins.Add(user);
//            _db.SaveChanges();

//            return Ok(user);// "User registered");
//        }

//        [HttpPost("login")]
//        public IActionResult Login(LoginDTO dto)
//        {
//            var user = _db.Logins.FirstOrDefault(u => u.Username == dto.Username);
//            if (user == null) return Unauthorized("Invalid credentials");

//            var service = new PasswordService();
//            if (!service.VerifyPassword(user.Password, dto.Password))
//                return Unauthorized("Invalid credentials");

//            // Create JWT
//            var claims = new[]
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                new Claim("UserId", user.LoginId.ToString()),
//                new Claim("Username", user.Username)
//            };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                issuer: _config["Jwt:Issuer"],
//                audience: _config["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.UtcNow.AddDays(1),
//                signingCredentials: creds
//            );
//            var tokenVar = new Tokens();
//            var refreshToken = tokenVar.GetRefreshToken();

//            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
//            return accessToken == null
//                    ? Unauthorized(new { message = "Unauthorized" })
//                    : Ok(new { accessToken = accessToken, refreshToken = refreshToken });

//        }

//        [Authorize]
//        [HttpGet("secure")]
//        public IActionResult SecureEndpoint()
//        {
//            var username = User.FindFirst("Username")?.Value;
//            return Ok($"Hello, {username}. You are authorized.");
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Stock.Models;
using Stock.Models.ModelDTO;
using Stock.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Stock.Data;

namespace Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        [HttpPost("register")]
        public IActionResult Register(LoginDTO dto)
        {
            var service = new PasswordService();
            var hashed = service.HashPassword(dto.Password);

            var user = new Login
            {
                Username = dto.Username,
                Password = hashed,
                AccessToken = string.Empty,
                RefreshToken = string.Empty,
                RefreshTokenExpiryTime = DateTime.UtcNow
            };

            _db.Logins.Add(user);
            _db.SaveChanges();

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var user = _db.Logins.FirstOrDefault(u => u.Username == dto.Username);
            if (user == null) return Unauthorized("Invalid credentials");

            var service = new PasswordService();
            if (!service.VerifyPassword(user.Password, dto.Password))
                return Unauthorized("Invalid credentials");

            // Create JWT access token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.LoginId.ToString()),
                new Claim("Username", user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = new Tokens().GetRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            _db.SaveChanges();

            return Ok(new { accessToken, refreshToken });
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(TokenDTO tokenDto)
        {
            if (tokenDto is null) return BadRequest("Invalid request");

            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var username = principal?.FindFirst("Username")?.Value;

            var user = _db.Logins.FirstOrDefault(u => u.Username == username);
            if (user == null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Unauthorized("Invalid refresh attempt");
            }

            // Generate new token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.LoginId.ToString()),
                new Claim("Username", user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var newToken = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(newToken);
            return Ok(new { accessToken, refreshToken = tokenDto.RefreshToken });
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false, // Ignore expiration
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            return (securityToken is JwtSecurityToken jwt && jwt.Header.Alg == SecurityAlgorithms.HmacSha256)
                ? principal
                : null;
        }

        [Authorize]
        [HttpGet("secure")]
        public IActionResult SecureEndpoint()
        {
            var username = User.FindFirst("Username")?.Value;
            return Ok($"Hello, {username}. You are authorized.");
        }
    }
}
