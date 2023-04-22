using PriceChecker.Common.Models;
using PriceChecker.Data.Repositories.Interfaces;

namespace PriceChecker.Data.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        public PriceCheckerContext Context { get; set; }

        public EntryRepository(PriceCheckerContext context)
        {
            Context = context;
        }

        public void AddNewEntry(Entry entry)
        {
            Context.AddAsync(entry);
        }

        public List<Entry> GetEntries()
        {
            return Context.Entries.ToList();
        }
    }
}
