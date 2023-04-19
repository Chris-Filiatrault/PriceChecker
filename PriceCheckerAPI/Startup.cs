using HtmlAgilityPack;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceCheckerAPI.Services;

[assembly: FunctionsStartup(typeof(PriceCheckerAPI.Startup))]

namespace PriceCheckerAPI
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<HtmlWeb>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddScoped<IPriceService>(serviceProvider =>
            {
                var client = serviceProvider.GetRequiredService<HtmlWeb>();
                var emailService = serviceProvider.GetRequiredService<IEmailService>();

                var config = serviceProvider.GetService<IConfiguration>();
                return new PriceService(client, emailService);
            });
        }
    }
}