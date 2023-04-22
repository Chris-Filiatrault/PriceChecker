using HtmlAgilityPack;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using PriceChecker.Data;
using PriceChecker.API.Services;
using PriceChecker.API.Services.Interfaces;
using PriceChecker.Data.Repositories;
using PriceChecker.Data.Repositories.Interfaces;

[assembly: FunctionsStartup(typeof(PriceChecker.API.Startup))]

namespace PriceChecker.API
{
    public class Startup : FunctionsStartup
    {
        private const string SqlConnectionString = "SqlConnectionString";

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<HtmlWeb>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IEntryRepository, EntryRepository>();
            builder.Services.AddScoped<IEntryService>(serviceProvider =>
            {
                var entryRepository = serviceProvider.GetRequiredService<IEntryRepository>();
                return new EntryService(entryRepository);
            });

            builder.Services.AddScoped<IPriceService>(serviceProvider =>
            {
                var client = serviceProvider.GetRequiredService<HtmlWeb>();
                var emailService = serviceProvider.GetRequiredService<IEmailService>();
                var entryService = serviceProvider.GetRequiredService<IEntryService>();

                var config = serviceProvider.GetService<IConfiguration>();
                return new PriceService(client, emailService, entryService);
            });

            builder.Services.AddDbContext<PriceCheckerContext>((serviceProvider, dataContextOptions) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                var dbConnectionString = config.GetValue<string>(SqlConnectionString);
                dataContextOptions.UseSqlServer(dbConnectionString);
            });
        }
    }
}