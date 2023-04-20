using Microsoft.EntityFrameworkCore;
using PriceChecker.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceChecker.Data
{
    public class PriceCheckerContext : DbContext
    {
        public PriceCheckerContext(DbContextOptions<PriceCheckerContext> options) : base(options)
        {
            
        }

        public DbSet<Entry>? Entries { get; set; }
    }
}