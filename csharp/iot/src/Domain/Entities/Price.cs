using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Principal;

namespace Domain.Entities
{
    public class Price
    {
        public Guid Id { get; set; }
        public DateTime PricePeriod { get; set; }
        public string Location { get; set; }
        public string Currency { get; set; }
        public string Unit { get; set; }
        public decimal Average { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public List<PriceDetail> PriceDetailList { get; private set; }

        public Price()
        {
            PriceDetailList = new List<PriceDetail>();
        }

        public Price(Guid id, DateTime pricePeriod, string location, string currency, string unit, decimal average, decimal max, decimal min)
        {
            Id = id;
            PricePeriod = pricePeriod;
            Location = location;
            Currency = currency;
            Unit = unit;
            Average = average;
            Max = max;
            Min = min;
        }

        public new string ToString()
        {
            var result =
                $"Date: {this.PricePeriod} Unit: {this.Unit} Currency: {this.Currency} Location: {this.Location}";

            return result;
        }
    }
}
