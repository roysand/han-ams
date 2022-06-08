using System;
using System.Linq;
using Domain.Entities;

namespace Application.Common.Helpers
{
    public static class PriceHelper
    {
        public static Price CreatePrice(this Publication_MarketDocument data)
        {
            var price = new Price()
            {
                PriceId = Guid.NewGuid(),
                Currency = data.TimeSeries.currency_Unitname,
                Unit = data.TimeSeries.price_Measure_Unitname,
                PricePeriod = Convert.ToDateTime(DateTime.Parse(data.periodtimeInterval.start).ToLocalTime().ToString("yyyy-MM-dd")),
                Location = "HOME"
            };

            return price;
        }

        public static Price CreatePriceDetail(this Publication_MarketDocument data)
        {
            var price = data.CreatePrice();
            decimal max = 0;
            decimal min = 10000;
            
            foreach (var dataPoint in data.TimeSeries.Period.Point)
            {
                var priceDetail = new PriceDetail()
                {
                    PriceDetailId = Guid.NewGuid(),
                    Price = price,
                    Amount = dataPoint.priceamount,
                    PricePeriod = price.PricePeriod.AddHours(Convert.ToInt16(dataPoint.position) - 1)
                };

                max = Math.Max(max, priceDetail.Amount);
                min = Math.Min(min, priceDetail.Amount);
                
                price.PriceDetailList.Add(priceDetail);
            }

            price.Max = max;
            price.Min = min;
            price.Average = price.PriceDetailList.Average(a => a.Amount);
            
            return price;
        }
    }
}