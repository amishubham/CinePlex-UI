using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineMovieTicketBookingSystem.Controllers

    {
        [Route("api/[controller]")]
        [ApiController]
        public class EmailController : ControllerBase
        {
            [HttpPost("SendConfirmation")]
            public IActionResult SendConfirmation([FromBody] EmailData emailData)
            {
                try
                {
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("your-email@gmail.com", "your-email-password"),
                        EnableSsl = true,
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("bookingmovie2@gmail.com"),
                        Subject = "Payment Confirmation",
                        Body = $"Your payment of INR {emailData.Amount} for show ID {emailData.ShowId} has been confirmed. Payment ID: {emailData.PaymentId}",
                        IsBodyHtml = false,
                    };
                    mailMessage.To.Add(emailData.UserEmail);

                    smtpClient.Send(mailMessage);

                    return Ok("Email sent successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            public class EmailData
            {
                public int UserId { get; set; }
                public int ShowId { get; set; }
                public decimal Amount { get; set; }
                public string PaymentId { get; set; }
                public string UserEmail { get; set; }
                public string SeatNos { get; set; }
        }
        }
        
    }


                

