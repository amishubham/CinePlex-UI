
namespace OnlineMovieTicketBookingSystem.Dtos
{
    public class CreateTicketDto
    {
        public int ShowId { get; set; }
        public int UserId { get; set; }
        public int NoOfTickets { get; set; }
        public string SeatNos { get; set; }
        public DateTime ShowTiming { get; set; }
    }
}