using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using catalogo.Domain.Interfaces;
using catalogo.Domain.Models;

namespace catalogo.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        // Registro → retorna el usuario creado
        public async Task<User?> Register(User user)
        {
            var existing = await _userRepository.GetByEmail(user.Email);
            if (existing != null) return null;

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userRepository.Create(user);
            return user;
        }

        // Login → retorna el token generado
        public async Task<string?> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null) return null;

            bool passwordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!passwordValid) return null;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
