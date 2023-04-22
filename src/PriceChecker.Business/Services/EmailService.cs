using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using PriceChecker.Business.Services.Interfaces;
using PriceChecker.Common;

namespace PriceChecker.Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly string? SmtpApiKey = Environment.GetEnvironmentVariable(Constants.SmtpApiKey);

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
