

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineMovieTicketBookingSystem.Dtos;
using OnlineMovieTicketBookingSystem.Models;
using OnlineMovieTicketBookingSystem.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace OnlineMovieTicketBookingSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<User> _userRepository;

        public AuthService(IConfiguration configuration, IGenericRepository<User> userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public bool UserNameExists(string username)
        {
            var user = _userRepository.GetAll().Data.FirstOrDefault(u => u.UserName == username);
            return user != null;
        }

        public bool AuthenicareUser(LoginDto loginDto)
        {
            var user = _userRepository.GetAll().Data.FirstOrDefault(u => u.UserName == loginDto.UserName);
            return user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
        }

        public string GenerateJwtToken(LoginDto loginDto)
        {
            var user = _userRepository.GetAll().Data.FirstOrDefault(u => u.UserName == loginDto.UserName);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecurityKey"] ?? throw new InvalidOperationException("Security key not found."));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName ?? throw new InvalidOperationException("User name is null.")),
                    new Claim(ClaimTypes.Role, user.UserType ?? throw new InvalidOperationException("User type is null."))
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public User GetUserByUserName(string username)
        {
            return _userRepository.GetAll().Data.FirstOrDefault(u => u.UserName == username);
        }
    }
}
