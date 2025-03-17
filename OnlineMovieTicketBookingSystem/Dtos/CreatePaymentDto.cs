namespace OnlineMovieTicketBookingSystem.Dtos
{
    public class CreatePaymentDto
    {
        public int TicketId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}