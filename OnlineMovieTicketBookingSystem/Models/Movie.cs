using System.ComponentModel.DataAnnotations;

namespace OnlineMovieTicketBookingSystem.Models
{
    public class Movie
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public string ReleaseDate { get; set; }

    }
}
