using OnlineMovieTicketBookingSystem.Models;
using OnlineMovieTicketBookingSystem.Repositories;

namespace OnlineMovieTicketBookingSystem.Services
{
    public class ShowService: IShowService

    {
            private readonly IGenericRepository<Movie> _movierepo;
            private readonly IGenericRepository<Theatre> _theatrerepo;

            public ShowService(IGenericRepository<Movie> movierepo, IGenericRepository<Theatre> theatrerepo)

            {
                _movierepo = movierepo;
                _theatrerepo = theatrerepo;
            }

            public bool ValidShow(Show show)
            {
                var movieid=show.MovieId;
                var movie = _movierepo.GetById(movieid);
            if(movie.Data == null)
                return false;
            var theatreid = show.TheatreId;
            var theatre = _theatrerepo.GetById(theatreid);
            if (theatre.Data== null)
                return false;
            if(show.TicketsAvailable > theatre.Data.SeatCapacity)
                return false;
            return true;

        }

    }
}
