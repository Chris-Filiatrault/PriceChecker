using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace PriceChecker.API.Services
{
    internal class PriceService : IPriceService
    {
        private readonly HtmlWeb client;
        private readonly IEmailService emailService;

        internal const string ChocBrownieUrl = "https://www.chemistwarehouse.com.au/buy/76850/musashi-high-protein-bar-chocolate-brownie-90g";
        internal const string ChocBrownieElementId = "p_lt_ctl10_pageplaceholder_p_lt_ctl00_wBR_P_D1_ctl00_ctl00_ctl00_ctl00_ctl02_lblActualPrice";


        internal PriceService(HtmlWeb client, IEmailService emailService)
        {
            this.client = client;
            this.emailService = emailService;
        }

        public void CheckPrices(ILogger log)
        {
            var browniePrice = GetPriceFromElementId(ChocBrownieUrl, ChocBrownieElementId);
            if (browniePrice >= 0 && browniePrice < 3)
            {
                log.LogInformation("Price is {price}. Sending email.", browniePrice);
                emailService.SendEmail(browniePrice);
            }

            log.LogInformation("Price was not less than $3. Did not send email.");
        }

        private decimal GetPriceFromElementId(string url, string elementId)
        {
            HtmlDocument html = client.Load(url);
            var priceString = html.GetElementbyId(elementId).InnerHtml;
            priceString = priceString.Replace("$", string.Empty);
            decimal.TryParse(priceString, out decimal price);

            return price;
        }
    }
}
