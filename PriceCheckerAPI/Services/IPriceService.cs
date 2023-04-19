using Microsoft.Extensions.Logging;

namespace PriceCheckerAPI.Services
{
    public interface IPriceService
    {
        internal void CheckPrices(ILogger log);
    }
}