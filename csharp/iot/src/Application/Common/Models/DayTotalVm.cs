using System;
using System.Collections.Generic;

namespace Application.Common.Models
{
    public class DayTotalVm
    {
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public decimal ValueDaySoFar { get; set; } = 0;
        public decimal? PriceExTaxNOK { get; set; }
        public decimal? PriceNOK { get; set; }


        public IList<HourTotalDataVm> Data { get; }
        
        public DayTotalVm()
        {
            Data = new List<HourTotalDataVm>();
        }
    }
    
    public class HourTotalDataVm
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; } = 0;
        public string Description { get; set; }
        public string Unit { get; set; }
        public decimal? PriceExTaxNOK {get; set; }
        public decimal? PriceNOK { get; set; }
    }
}