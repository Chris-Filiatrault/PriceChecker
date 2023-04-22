using PriceChecker.Common.Models;

namespace PriceChecker.Data.Repositories.Interfaces
{
    public interface IEntryRepository
    {
        PriceCheckerContext Context { get; set; }

        void AddNewEntry(Entry entry);

        List<Entry> GetEntries();
    }
}