﻿using System;
using System.Linq;
using Application.Common.Models;
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
                InDomain = data.TimeSeries.in_DomainmRID.Value,
                OutDomain = data.TimeSeries.out_DomainmRID.Value,
                Location = "HOME"
            };

            return price;
        }

        public static Price CreatePriceDetail(this Publication_MarketDocument data)
        {
            var price = data.CreatePrice();
            decimal max = 0;
            decimal min = 10000;
            int rowCount = -1;
            
            foreach (var dataPoint in data.TimeSeries.Period.Point)
            {
                rowCount++;
                var priceDetail = new PriceDetail()
                {
                    PriceDetailId = Guid.NewGuid(),
                    Price = price,
                    Amount = dataPoint.priceamount,
                    //PricePeriod = price.PricePeriod.AddHours(Convert.ToInt16(dataPoint.position) - 1)
                    PricePeriod = price.PricePeriod.AddHours(rowCount)
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