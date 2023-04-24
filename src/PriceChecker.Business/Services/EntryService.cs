using PriceChecker.Business.Services.Interfaces;
using PriceChecker.Common.Models;
using PriceChecker.Data.Repositories.Interfaces;

namespace PriceChecker.Business.Services
{
    public class EntryService : IEntryService
    {
        private readonly IEntryRepository entryRepository;

        public EntryService(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        void IEntryService.AddNewEntry(Entry entry)
        {
            entryRepository.AddNewEntry(entry);
        }

        List<Entry> IEntryService.GetEntries()
        {
            var entries = entryRepository.GetEntries();
            return entries;
        }
    }
}
