using Microsoft.EntityFrameworkCore;
using PriceChecker.Common.Models;

namespace PriceChecker.Data
{
    public class PriceCheckerContext : DbContext
    {
        public PriceCheckerContext(DbContextOptions<PriceCheckerContext> options) : base(options)
        {
            
        }

        public DbSet<Entry> Entries { get; set; }
    }
}