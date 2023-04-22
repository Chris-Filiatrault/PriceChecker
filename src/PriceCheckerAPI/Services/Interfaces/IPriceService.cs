using Microsoft.Extensions.Logging;

namespace PriceChecker.Business.Services.Interfaces
{
    public interface IPriceService
    {
        internal void CheckPrices(ILogger log);
    }
}