using System;

namespace Domain.Entities
{
    public class Day
    {
        public DateTime TimeStamp { get; set; }
        public string Location { get; set; }
        public string Unit { get; set; }
        public decimal ValueNum { get; set; }

        public Day()
        {
        }
    }
}
    