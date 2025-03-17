using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineMovieTicketBookingSystem.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Show")]
        public int ShowId { get; set; }
        public Show Show { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public int NoOfTickets { get; set; }
        public string SeatNos { get; set; }
        public DateTime ShowTiming { get; set; }

        public string MovieName { get; set; }
        public string TheatreName { get; set; }
    }
}
