using System;
using System.Collections;
using System.Collections.Generic;

namespace Application.Common.Models
{
    public class DailyTotalVm
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string Unit { get; set; }
        public decimal? PriceExTaxNOK { get; set; }
        public decimal? PriceNOK { get; set; }

        public IList<CurrentHour> CurrentHour { get; set; }
        public IList<DayTotalVm> HourData { get; set; }
        public IList<PriceVm> Prices { get; set; }

        public DailyTotalVm()
        {
            HourData = new List<DayTotalVm>();
        }
    }
}