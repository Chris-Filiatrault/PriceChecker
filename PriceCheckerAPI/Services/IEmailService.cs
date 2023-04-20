namespace PriceChecker.API.Services
{
    public interface IEmailService
    {
        void SendEmail(decimal price);
    }
}