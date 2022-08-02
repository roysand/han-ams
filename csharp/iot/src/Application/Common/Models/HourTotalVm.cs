using System;
using System.Collections.Generic;

namespace Application.Common.Models
{
    public class HourTotalVm
    {
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public decimal ValueDaySoFar { get; set; }

        public IList<HourTotalDataVm> Data { get; }
        
        public HourTotalVm()
        {
            Data = new List<HourTotalDataVm>();
        }
    }
    
    public class HourTotalDataVm
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }   
    }
}