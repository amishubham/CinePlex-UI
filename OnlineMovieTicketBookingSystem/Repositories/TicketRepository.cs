using Microsoft.EntityFrameworkCore;
using OnlineMovieTicketBookingSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMovieTicketBookingSystem.Repositories
{
    public class TicketRepository : IGenericRepository<Ticket>
    {
        private readonly MovieContext _context;

        public TicketRepository(MovieContext context)
        {
            _context = context;
        }

        public Result<Ticket> Add(Ticket entity)
        {
            var show = _context.Shows.Find(entity.ShowId);
            if (show == null)
            {
                return new Result<Ticket> { IsSuccess = false, Message = "Show not found" };
            }

            entity.MovieName = show.MovieName;
            entity.TheatreName = show.TheatreName;

            _context.Tickets.Add(entity);
            _context.SaveChanges();
            return new Result<Ticket> { IsSuccess = true, Data = entity };
        }

        public Result<Ticket> Delete(int id)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
            {
                return new Result<Ticket> { IsSuccess = false, NotFound = true };
            }

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
            return new Result<Ticket> { IsSuccess = true, Data = ticket };
        }

        public Result<List<Ticket>> GetAll()
        {
            var tickets = _context.Tickets.Include(t => t.User).ToList();
            return new Result<List<Ticket>> { IsSuccess = true, Data = tickets };
        }

        public Result<Ticket> GetById(int id)
        {
            var ticket = _context.Tickets
                                 .Include(t => t.User)
                                 .FirstOrDefault(t => t.Id == id);
            if (ticket == null)
            {
                return new Result<Ticket> { IsSuccess = false, NotFound = true, Data = null };
            }
            return new Result<Ticket> { IsSuccess = true, NotFound = false, Data = ticket };
        }

        public Result<Ticket> GetMovieByTheatre(string theatreName)
        {
            var ticket = _context.Tickets
                                 .Include(t => t.Show)
                                 .FirstOrDefault(t => t.Show.TheatreName == theatreName);
            if (ticket == null)
            {
                return new Result<Ticket> { IsSuccess = false, NotFound = true, Data = null };
            }
            return new Result<Ticket> { IsSuccess = true, NotFound = false, Data = ticket };
        }

        public Result<Ticket> Update(Ticket entity)
        {
            var show = _context.Shows.Find(entity.ShowId);
            if (show == null)
            {
                return new Result<Ticket> { IsSuccess = false, Message = "Show not found" };
            }

            entity.MovieName = show.MovieName;
            entity.TheatreName = show.TheatreName;

            _context.Tickets.Update(entity);
            _context.SaveChanges();
            return new Result<Ticket> { IsSuccess = true, Data = entity };
        }
    }
}