using System.Net.Mail;
using System.Net;
using PriceChecker.Business.Services.Interfaces;
using Microsoft.Extensions.Options;
using PriceChecker.Business.Options;
using Microsoft.Extensions.Logging;

namespace PriceChecker.Business.Services
{
    public class EmailService : IEmailService
    {
        private const string smtpServer = "smtp-relay.sendinblue.com";
        private readonly SmtpOptions options;
        private readonly ILogger<EmailService> log;

        public EmailService(IOptions<SmtpOptions> options, ILogger<EmailService> log)
        {
            this.options = options.Value;
            this.log = log;
        }

        public void SendEmail(decimal price)
        {
            try
            {
                var smtpClient = new SmtpClient(smtpServer)
                {
                    Port = 587,
                    Credentials = new NetworkCredential(options.ChrisEmailAddress, options.ApiKey),
                    EnableSsl = true
                };

                log.LogInformation("Sending email");
                smtpClient.Send(new MailMessage(options.ChrisEmailAddress, options.ChrisEmailAddress)
                {
                    Subject = "Musashi Price",
                    Body = $"Price is {price}"
                });
            }
            catch(Exception exception)
            {
                log.LogError(exception, "Failed to send email");
                throw;
            }
        }
    }
}
