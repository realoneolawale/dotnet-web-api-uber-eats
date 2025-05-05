
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ubereats.Data;
using Ubereats.DTO;
using Ubereats.Helpers;
using Ubereats.Models;

namespace Ubereats.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UberEatsContext _context;
        private readonly IConfiguration _config;

        public UserRepository(UberEatsContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == HashPasswordToSHA256(loginDto.Password));
            if (user != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:key"])); // using Microsoft.IdentityModel.Tokens
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // using Microsoft.IdentityModel.Tokens
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(60),
                signingCredentials: credentials);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token); // using Microsoft.IdentityModel.Tokens.jwt
                return new LoginResponseDto(
                    jwt,
                    "bearer",
                    user.Id,
                    user.Email,
                    user.Name
                );
            }
            throw new UberEatsException("Invalid login", HttpStatusCode.NotFound);
        }

        public async Task<string> Register(UserDto userDto)
        {
            var userExists = _context.Users.FirstOrDefault(u => u.Email == userDto.Email);
            if (userExists != null)
                throw new UberEatsException("User already exists", HttpStatusCode.Conflict);
            var user = new User() { Email = userDto.Email, Name = userDto.Name, Phone = userDto.Phone, Password = HashPasswordToSHA256(userDto.Password) };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "User created successfully";
        }

        public string HashPasswordToSHA256(string s)
        {
            var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}