using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PriceChecker.Common.Models;
using PriceChecker.Data.Repositories.Interfaces;

namespace PriceChecker.Data.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly ILogger<EntryRepository> log;

        public DbSet<Entry> DataSet { get; }

        public PriceCheckerContext Context { get; }

        public EntryRepository(PriceCheckerContext context, DbSet<Entry> dataSet, ILogger<EntryRepository> log)
        {
            Context = context;
            this.log = log;
            Context.Database.EnsureCreated();
            DataSet = Context.Entries;
        }

        public void AddNewEntry(Entry entry)
        {
            entry.Id = Guid.NewGuid();

            log.LogInformation("Adding new entry to database.");
            DataSet.AddAsync(entry);

            log.LogInformation("Saving changes to database.");
            Context.SaveChangesAsync();
        }

        public List<Entry> GetEntries()
        {
            return DataSet.AsNoTracking().ToList();
        }
    }
}
