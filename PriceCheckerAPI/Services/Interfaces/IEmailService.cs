namespace PriceChecker.API.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(decimal price);
    }
}