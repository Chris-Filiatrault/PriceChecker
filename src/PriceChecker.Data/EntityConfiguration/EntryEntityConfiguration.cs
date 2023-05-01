using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceChecker.Common.Models;

namespace PriceChecker.Data.EntityConfiguration
{
    internal class EntryEntityConfiguration : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.Property(entry => entry.Price).IsRequired();
            builder.Property(entry => entry.DateRecorded).IsRequired();
            builder.Property(entry => entry.Product).IsRequired();
            builder.Property(entry => entry.Website).IsRequired();
        }
    }
}