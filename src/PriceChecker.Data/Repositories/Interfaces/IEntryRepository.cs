using PriceChecker.Common.Models;

namespace PriceChecker.Data.Repositories.Interfaces
{
    public interface IEntryRepository
    {
        void AddNewEntry(Entry entry);

        List<Entry> GetEntries();
    }
}