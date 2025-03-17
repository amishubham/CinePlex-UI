using OnlineMovieTicketBookingSystem.Models;

namespace OnlineMovieTicketBookingSystem.Services
{
    public interface ITicketService
    {
        bool TicketAvailable(Ticket ticket);
    }
}
