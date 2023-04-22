using System;
using HtmlAgilityPack;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PriceChecker.Data;
using PriceChecker.Business.Services;
using PriceChecker.Business.Services.Interfaces;
using PriceChecker.Data.Repositories;
using PriceChecker.Data.Repositories.Interfaces;
using PriceChecker.Common;


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

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var config = builder.ConfigurationBuilder.Build();
            Environment.SetEnvironmentVariable(Constants.SmtpApiKey, config.GetValue<string>(Constants.SmtpApiKey));
            var key = config.GetValue<string>(Constants.SmtpApiKey);
            base.ConfigureAppConfiguration(builder);
        }
    }
}