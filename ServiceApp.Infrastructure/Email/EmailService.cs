using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using ServiceApp.Domain.Email;


namespace ServiceApp.Infrastructure.Email;
public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var emailSettings = _config.GetSection("EmailSettings");
        var smtpServer = emailSettings["SMTPServer"];
        var portString = emailSettings["Port"];
        var username = emailSettings["Username"];
        var password = emailSettings["Password"];

        if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(portString) ||
            string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            throw new InvalidOperationException("Email settings are not configured properly.");
        }

        var port = int.TryParse(portString, out var parsedPort) ? parsedPort : 587; // Default to 587 if parsing fails

        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("ServiceApp", username));
        emailMessage.To.Add(new MailboxAddress("", toEmail));
        emailMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = body };
        emailMessage.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(smtpServer, port, SecureSocketOptions.Auto);
        await client.AuthenticateAsync(username, password);
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}
