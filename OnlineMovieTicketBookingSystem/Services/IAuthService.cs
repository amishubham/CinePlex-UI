using OnlineMovieTicketBookingSystem.Dtos;
using OnlineMovieTicketBookingSystem.Models;

namespace OnlineMovieTicketBookingSystem.Services
{
    //public interface IAuthService
    //{
    //    bool UserNameExists(string username);
    //    bool AuthenicareUser(LoginDto loginDto);
    //    string GenerateJwtToken(LoginDto loginDto);
    //}
    public interface IAuthService
    {
        bool UserNameExists(string username);
        bool AuthenicareUser(LoginDto loginDto);
        string GenerateJwtToken(LoginDto loginDto);
        User GetUserByUserName(string username);
    }

}
