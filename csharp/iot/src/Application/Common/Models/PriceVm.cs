using System;

namespace Application.Common.Models
{
    public class PriceVm
    {
        public DateTime PricePeriod { get; set; }
        public decimal? PriceNOK { get; set; }
        public decimal? PriceEUR { get; set; }
        public decimal? ExchangeRate { get; set; }
    }
}