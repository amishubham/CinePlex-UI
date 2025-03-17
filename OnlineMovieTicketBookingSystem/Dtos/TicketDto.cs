using OnlineMovieTicketBookingSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMovieTicketBookingSystem.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }
      
        public int ShowId { get; set; }
        
        public int UserId { get; set; }
        public int NoOfTickets { get; set; }
        public string SeatNos { get; set; }
        public DateTime ShowTiming { get; set; }
    }
}
