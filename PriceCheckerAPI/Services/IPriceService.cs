using Microsoft.Extensions.Logging;

namespace PriceChecker.API.Services
{
    public interface IPriceService
    {
        internal void CheckPrices(ILogger log);
    }
}