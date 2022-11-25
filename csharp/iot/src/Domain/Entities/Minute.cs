using System;

namespace Domain.Entities
{
    public class Minute
    {
        public DateTime TimeStamp { get; set; }
        public string Location { get; set; }
        public string Unit { get; set; }
        public decimal ValueNum { get; set; }
        public Int16 Count { get; set; }

        public Minute()
        {
        }
    }
}