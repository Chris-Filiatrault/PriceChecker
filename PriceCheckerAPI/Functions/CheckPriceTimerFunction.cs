using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PriceCheckerAPI.Services;

namespace PriceCheckerAPI.Functions
{
    public class CheckPriceTimerFunction
    {
        private readonly IPriceService priceService;

        public CheckPriceTimerFunction(IPriceService priceService)
        {
            this.priceService = priceService;
        }

        [FunctionName("CheckPricesTimerFunction")]
        public void Run([TimerTrigger("0 * 22 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                priceService.CheckPrices(log);
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Failed to check prices.");
            }
        }
    }
}