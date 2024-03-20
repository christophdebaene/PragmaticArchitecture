using MailKit.Net.Smtp;
using MimeKit;
using TodoApp.Domain.Services;

namespace TodoApp.Infrastructure.Services;
public class MimeKitEmailSender : IEmailSender
{
    public async Task SendEmailAsync(string to, string from, string subject, string body)
    {
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("", 25, false);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from, from));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
