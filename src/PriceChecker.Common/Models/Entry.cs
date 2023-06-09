﻿namespace PriceChecker.Common.Models
{
    public class Entry
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public DateTime DateRecorded { get; set; }

        public string Product { get; set; } = string.Empty;

        public string Website { get; set; } = string.Empty;
    }
}