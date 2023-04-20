using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PriceChecker.API.Services;

namespace PriceChecker.API.Functions
{
    public class CheckPriceHttpFunction
    {
        private readonly IPriceService priceService;

        public CheckPriceHttpFunction(IPriceService priceService)
        {
            this.priceService = priceService;
        }

        [FunctionName("CheckPricesHttpFunction")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                priceService.CheckPrices(log);
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Failed to check prices.");
            }

            return new OkObjectResult("Checking prices.");
        }
    }
}
