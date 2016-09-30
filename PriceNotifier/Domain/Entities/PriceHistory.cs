﻿namespace Domain.Entities
{
    public class PriceHistory
    {
        public int PriceHistoryId { get; set; }
        public string Date { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}