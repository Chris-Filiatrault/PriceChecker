using Microsoft.EntityFrameworkCore;
using PriceChecker.Common.Models;
using PriceChecker.Data.EntityConfiguration;

namespace PriceChecker.Data
{
    public class PriceCheckerContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PriceCheckerContext(DbContextOptions<PriceCheckerContext> options) : base(options)
        {
            
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public virtual DbSet<Entry> Entries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EntryEntityConfiguration());
        }
    }
}