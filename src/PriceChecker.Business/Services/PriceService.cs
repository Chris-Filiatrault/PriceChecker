using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using PriceChecker.Business.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

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

        public async void CheckPrices()
        {
            var browniePrice = await GetPriceFromElementId(ChocBrownieUrl, ChocBrownieElementId);
            log.LogInformation("Price is {BrowniePrice}", browniePrice);

            if (browniePrice > 0 && browniePrice < 3)
            {
                emailService.SendEmail(browniePrice);
            }
        }

        private async Task<decimal> GetPriceFromElementId(string url, string elementId)
        {
            try
            {
                var httpClient = new HttpClient();
                var result = await httpClient.GetAsync(url);
                var content = result.Content;
                log.LogInformation("Http client content", content);


                log.LogInformation("Loading URL", url);
                HtmlDocument html = client.Load(url);
                log.LogInformation("HTML: {0}", html.Text);
                
                HtmlDocument html2 = client.Load(url);
                log.LogInformation("HTML 2: {0}", html2.Text);
                
                HtmlDocument html3 = client.Load(url);
                log.LogInformation("HTML 3: {0}", html3.Text);

                var element = html.GetElementbyId(elementId);
                log.LogInformation("Element: {0}", element);

                var innerHTML = element.InnerHtml;
                log.LogInformation("Inner HTML: {0}", innerHTML);

                var priceString = innerHTML.Replace("$", string.Empty);
                log.LogInformation($"Price is: {priceString}");

                decimal.TryParse(priceString, out decimal price);
                log.LogInformation($"Finished parsing, price is {price}");
                
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
