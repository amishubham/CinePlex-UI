using OnlineMovieTicketBookingSystem.Models;

namespace OnlineMovieTicketBookingSystem.Repositories
{
    public interface IUserRepository
    {
        Result<User> GetByUserName(string username);
    }
}
