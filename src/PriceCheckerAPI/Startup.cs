using HtmlAgilityPack;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using PriceChecker.Business.Options;
using PriceChecker.Business.Services;
using PriceChecker.Business.Services.Interfaces;

[assembly: FunctionsStartup(typeof(PriceChecker.API.Startup))]

namespace PriceChecker.API
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Html Client
            builder.Services.AddScoped<HtmlWeb>();

            // Services
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IPriceService>(serviceProvider =>
            {
                var client = serviceProvider.GetRequiredService<HtmlWeb>();
                var emailService = serviceProvider.GetRequiredService<IEmailService>();
                var log = serviceProvider.GetRequiredService<ILogger<PriceService>>();

                var config = serviceProvider.GetService<IConfiguration>();
                return new PriceService(client, emailService, log);
            });

            // Options
            builder.Services.AddOptions<SmtpOptions>()
                .Configure<IConfiguration>((options, config) =>
                {
                    config.GetSection("Smtp").Bind(options);
                });
        }
    }
}