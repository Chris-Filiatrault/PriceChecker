using HtmlAgilityPack;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using PriceChecker.Data;
using PriceChecker.API.Services;

[assembly: FunctionsStartup(typeof(PriceChecker.API.Startup))]

namespace PriceChecker.API
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

            builder.Services.AddDbContext<PriceCheckerContext>((serviceProvider, dataContextOptions) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                var dbConnectionString = config.GetValue<string>("SqlConnectionString");
                dataContextOptions.UseSqlServer(dbConnectionString);
            });
        }
    }
}