using System;
using System.Collections;
using System.Collections.Generic;

namespace Application.Common.Models
{
    public class DailyTotalVm
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }

        public string Description { get; set; }
        public string Unit { get; set; }

        public IList<HourTotalVm> HourData { get; set; }

        public DailyTotalVm()
        {
            HourData = new List<HourTotalVm>();
        }
    }
}