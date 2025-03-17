using Microsoft.EntityFrameworkCore.Metadata;
using OnlineMovieTicketBookingSystem.Models;
using OnlineMovieTicketBookingSystem.Repositories;

namespace OnlineMovieTicketBookingSystem.Services
{
    public class TicketService: ITicketService
    {
        private readonly IGenericRepository<Show> _repo;
        public TicketService(IGenericRepository<Show> Repo)
        {
            _repo = Repo;
        }
        public bool TicketAvailable(Ticket ticket)
        {
            var show = _repo.GetById(ticket.ShowId);
            if (show.Data.TicketsAvailable<ticket.NoOfTickets)
                return false;
            show.Data.TicketsAvailable = show.Data.TicketsAvailable- ticket.NoOfTickets;
            _repo.Update(show.Data);
            return true;
        }
    }
}
