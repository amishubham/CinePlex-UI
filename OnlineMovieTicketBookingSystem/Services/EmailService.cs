using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");

        var smtpPort = emailSettings["Port"];
        if (string.IsNullOrEmpty(smtpPort))
        {
            throw new ArgumentNullException(nameof(smtpPort), "SMTP port is not configured.");
        }

        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
        emailMessage.To.Add(new MailboxAddress(toEmail, toEmail));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("plain") { Text = message };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(emailSettings["Server"], int.Parse(smtpPort), false);
            await client.AuthenticateAsync(emailSettings["Username"], emailSettings["Password"]);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
