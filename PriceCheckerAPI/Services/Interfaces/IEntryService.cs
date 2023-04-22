using PriceChecker.Common.Models;
using System.Collections.Generic;

namespace PriceChecker.API.Services.Interfaces
{
    public interface IEntryService
    {
        void AddNewEntry(Entry entry);

        List<Entry> GetEntries();
    }
}