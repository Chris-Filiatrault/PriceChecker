using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using PriceChecker.Business.Services.Interfaces;
using PriceChecker.Common.Models;
using System;

namespace PriceChecker.Business.Services
{
    public class PriceService : IPriceService
    {
        private readonly HtmlWeb client;
        private readonly IEmailService emailService;
        private readonly IEntryService entryService;
        internal const string ChocBrownieProductName = "Musashi Choc Brownie Protein Bar";
        internal const string ChocBrownieUrl = "https://www.chemistwarehouse.com.au/buy/76850/musashi-high-protein-bar-chocolate-brownie-90g";
        internal const string ChocBrownieElementId = "p_lt_ctl10_pageplaceholder_p_lt_ctl00_wBR_P_D1_ctl00_ctl00_ctl00_ctl00_ctl02_lblActualPrice";


        public PriceService(HtmlWeb client, IEmailService emailService, IEntryService entryService)
        {
            this.client = client;
            this.emailService = emailService;
            this.entryService = entryService;
        }

        public void CheckPrices(ILogger log)
        {
            var browniePrice = GetPriceFromElementId(ChocBrownieUrl, ChocBrownieElementId);

            if (browniePrice >= 0 && browniePrice < 3)
            {
                log.LogInformation("Price is {price}. Sending email and creating database entry.", browniePrice);
                emailService.SendEmail(browniePrice);
                entryService.AddNewEntry(new Entry
                {
                    Price = browniePrice,
                    DateRecorded = DateTime.Today,
                    Product = ChocBrownieProductName,
                    Website = ChocBrownieUrl
                });
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
