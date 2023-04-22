using Microsoft.Extensions.Logging;

namespace PriceChecker.Business.Services.Interfaces
{
    public interface IPriceService
    {
        public void CheckPrices(ILogger log);
    }
}