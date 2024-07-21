using DatingApp.Client.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using DatingApp.Client.Data;

namespace DatingApp.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly string _key = "your-secret-key-should-be-long-and-secure"; // Replace with your secure key

        public AuthService(DataContext context)
        {
            _context = context;
        }

        public async Task<AuthResult> Register(RegisterModel model)
        {
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                return new AuthResult { Success = false, Message = "Email already in use.", Token = string.Empty };
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                NickName = model.NickName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Username = model.Email // Assuming email is used as username
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AuthResult { Success = true, Message = "User registered successfully", Token = string.Empty };
        }

        public async Task<AuthResult> Login(LoginModel model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return new AuthResult { Success = false, Message = "Invalid email or password.", Token = string.Empty };
            }

            var token = GenerateJwtToken(user);

            return new AuthResult { Success = true, Message = "User logged in successfully", Token = token };
        }

        public string GenerateJwtToken(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", user.Username) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public interface IAuthService
    {
        Task<AuthResult> Register(RegisterModel model);
        Task<AuthResult> Login(LoginModel model);
        string GenerateJwtToken(User user);
    }
}
