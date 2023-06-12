using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using PriceChecker.Business.Services.Interfaces;

namespace PriceChecker.Business.Services
{
    public class PriceService : IPriceService
    {
        private readonly HtmlWeb client;
        private readonly IEmailService emailService;
        private readonly ILogger<PriceService> log;
        internal const string ChocBrownieProductName = "Musashi Choc Brownie Protein Bar";
        internal const string ChocBrownieUrl = "https://www.chemistwarehouse.com.au/buy/76850/musashi-high-protein-bar-chocolate-brownie-90g";
        internal const string ChocBrownieElementId = "p_lt_ctl10_pageplaceholder_p_lt_ctl00_wBR_P_D1_ctl00_ctl00_ctl00_ctl00_ctl02_lblActualPrice";


        public PriceService(HtmlWeb client, IEmailService emailService, ILogger<PriceService> log)
        {
            this.client = client;
            this.emailService = emailService;
            this.log = log;
        }

        public void CheckPrices()
        {
            var browniePrice = GetPriceFromElementId(ChocBrownieUrl, ChocBrownieElementId);
            log.LogInformation("Price is {BrowniePrice}", browniePrice);

            if (browniePrice > 0 && browniePrice < 3)
            {
                emailService.SendEmail(browniePrice);
            }
        }

        private decimal GetPriceFromElementId(string url, string elementId)
        {
            try
            {
                HtmlDocument html = client.Load(url);
                var priceString = html.GetElementbyId(elementId).InnerHtml.Replace("$", string.Empty);
                decimal.TryParse(priceString, out decimal price);
                return price;
            }
            catch
            {
                log.LogError("Failed to get price from element id {ElementId} at url {URL}", elementId, url);
                throw;
            }
        }
    }
}
