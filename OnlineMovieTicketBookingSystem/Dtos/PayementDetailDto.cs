namespace OnlineMovieTicketBookingSystem.Dtos
{
    public class PaymentDetailDto
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
    }
}
