using TodoApp.Domain.Services;

namespace TodoApp.Infrastructure.Services;

public class RetryEmailSenderDecorator(IEmailSender emailSender) : IEmailSender
{
    private const int MAX_RETRIES = 10;
    public async Task SendEmailAsync(string to, string from, string subject, string body)
    {
        int attempts = 0;
        while(attempts < MAX_RETRIES)
        {
            try
            {
                await emailSender.SendEmailAsync(to, from, subject, body);
            }
            catch
            {
                attempts++;
                if (attempts == MAX_RETRIES)
                {
                    throw new InvalidOperationException($"Failed to send email after {attempts} attempts");
                }

                int delay = new Random().Next(500, 2000); // Delay between 0.5 to 2 seconds
                await Task.Delay(delay);                    
            }
        }
    }
}
