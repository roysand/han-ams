using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Principal;
using Domain.Common;

namespace Domain.Entities
{
    public class Price : AuditableEntity
    {
        public Guid PriceId { get; set; }
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
            PriceId = Guid.NewGuid();
        }

        public Price(Guid priceId, DateTime pricePeriod, string location, string currency, string unit, decimal average, decimal max, decimal min)
        {
            PriceId = priceId;
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
