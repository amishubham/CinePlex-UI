using System.Data;

namespace OnlineMovieTicketBookingSystem.Models
{
    public class Show
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public int MovieId { get; set; }
        public string TheatreName { get; set; }
        public int TheatreId { get; set; }  
        public int Price { get; set; }
        public int TicketsAvailable { get; set; }
        public DateTime ShowTiming { get; set; } 
    }
}
