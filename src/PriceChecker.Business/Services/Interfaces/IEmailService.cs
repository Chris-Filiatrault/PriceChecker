namespace PriceChecker.Business.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(decimal price);
    }
}