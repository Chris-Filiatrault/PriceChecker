namespace PriceCheckerAPI.Services
{
    public interface IEmailService
    {
        void SendEmail(decimal price);
    }
}