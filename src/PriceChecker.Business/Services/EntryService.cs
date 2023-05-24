using Microsoft.Extensions.Logging;
using PriceChecker.Business.Services.Interfaces;
using PriceChecker.Common.Models;
using PriceChecker.Data.Repositories.Interfaces;

namespace PriceChecker.Business.Services
{
    public class EntryService : IEntryService
    {
        private readonly IEntryRepository entryRepository;
        private readonly ILogger<EntryService> log;

        public EntryService(IEntryRepository entryRepository, ILogger<EntryService> log)
        {
            this.entryRepository = entryRepository;
            this.log = log;
        }

        void IEntryService.AddNewEntry(Entry entry)
        {
            try
            {
                entryRepository.AddNewEntry(entry);
            }
            catch(Exception exception)
            {
                log.LogError(exception, "Failed to add entry {Entry}", entry);
            }
        }

        List<Entry> IEntryService.GetEntries()
        {
            var entries = entryRepository.GetEntries();
            return entries;
        }
    }
}
