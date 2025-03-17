using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMovieTicketBookingSystem.Models
{
    public class PaymentDetail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
    }
}
