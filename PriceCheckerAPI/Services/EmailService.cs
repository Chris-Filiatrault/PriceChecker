using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace PriceCheckerAPI.Services
{
    internal class EmailService : IEmailService
    {
        private readonly string SmtpApiKey = string.Empty;

        public EmailService(IConfiguration config)
        {
            SmtpApiKey = config.GetValue<string>("SmtpApiKey");
        }

        public void SendEmail(decimal price)
        {
            var smtpClient = new SmtpClient("smtp-relay.sendinblue.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("chris.filiatrault@hotmail.com", SmtpApiKey),
                EnableSsl = true
            };

            smtpClient.Send(new MailMessage("chris.filiatrault@hotmail.com", "chris.filiatrault@hotmail.com")
            {
                Subject = "Musashi Price",
                Body = $"Price is {price}"
            });
        }
    }
}
