using Microsoft.EntityFrameworkCore;
using PriceChecker.Common.Models;
using PriceChecker.Data.Repositories.Interfaces;
using System.Data;

namespace PriceChecker.Data.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        public DbSet<Entry> DataSet { get; }

        public PriceCheckerContext Context { get; }

        public EntryRepository(PriceCheckerContext context, DbSet<Entry> dataSet)
        {
            Context = context;
            Context.Database.EnsureCreated();
            DataSet = Context.Entries;
        }

        public void AddNewEntry(Entry entry)
        {
           
            
            DataSet.AddAsync(entry);
            Context.SaveChangesAsync();
        }

        public List<Entry> GetEntries()
        {
            return DataSet.AsNoTracking().ToList();
        }
    }
}
