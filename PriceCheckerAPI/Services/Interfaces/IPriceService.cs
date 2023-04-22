using Microsoft.Extensions.Logging;

namespace PriceChecker.API.Services.Interfaces
{
    public interface IPriceService
    {
        internal void CheckPrices(ILogger log);
    }
}