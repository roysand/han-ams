using System;
using System.Collections.Generic;

namespace Application.Common.Models
{
    public class HourTotalVm
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        
        public HourTotalVm()
        {
        }
    }

    public class HourData
    {
        public string Location { get; set; }
        public IList<HourTotalVm> HourTotal { get; set; }

        public HourData()
        {
            
        }
    }
}