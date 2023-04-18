using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Web.Http;
using HtmlAgilityPack;
using System.Net.Mail;
using System.Net;

namespace PriceCheckerAPI
{
    public class CheckPriceFunction
    {
        private readonly string ChocBrownieUrl = "https://www.chemistwarehouse.com.au/buy/76850/musashi-high-protein-bar-chocolate-brownie-90g";
        private readonly string ChocBrownieElementId = "p_lt_ctl10_pageplaceholder_p_lt_ctl00_wBR_P_D1_ctl00_ctl00_ctl00_ctl00_ctl02_lblActualPrice";

        private readonly string OatsUrl = "https://www.coles.com.au/product/heritage-mill-clusters-berry-bliss-750g-5283445";
        private readonly string OatsElementId = "p_lt_ctl10_pageplaceholder_p_lt_ctl00_wBR_P_D1_ctl00_ctl00_ctl00_ctl00_ctl02_lblActualPrice";

        // TODO: Add to Env variables in Startup
        private readonly string SmtpApiKey = Environment.GetEnvironmentVariable("SmtpApiKey");

        [FunctionName("CheckPriceFunction")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            decimal browniePrice;

            try
            {
                browniePrice = GetPriceFromElementId(ChocBrownieUrl, ChocBrownieElementId);
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Unable to call url {URL}", ChocBrownieUrl);
                return new InternalServerErrorResult();
            }

            if (browniePrice == 0)
            {
                log.LogError("Price was $0");
                return new InternalServerErrorResult();
            }

            log.LogInformation("Price is {price}. Sending email", browniePrice);
            SendEmail(browniePrice);

            return new OkObjectResult($"Price is {browniePrice}");
        }

        internal void SendEmail(decimal price)
        {
            var smtpClient = new SmtpClient("smtp-relay.sendinblue.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("chris.filiatrault@hotmail.com", SmtpApiKey),
                EnableSsl = true
            };

            smtpClient.Send(new MailMessage("chris.filiatrault@hotmail.com", "chris.filiatrault@hotmail.com")
            {
                Subject = "Musashi Price",
                Body = $"Price is {price}"
            });
        }

        internal decimal GetPriceFromElementId(string url, string elementId)
        {
            var client = new HtmlWeb();
            HtmlDocument html = client.Load(url);
            var priceString = html.GetElementbyId(elementId).InnerHtml;
            priceString = priceString.Replace("$", String.Empty);
            decimal.TryParse(priceString, out decimal price);

            return price;
        }
    }
}
