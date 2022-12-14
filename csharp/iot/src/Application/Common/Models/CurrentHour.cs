using System;

namespace Application.Common.Models
{
    public class CurrentHour
    {
        public DateTime Date { get; set; }
        public decimal ValueNum { get; set; }
        public decimal PriceExTax { get; set; }
        public decimal PriceNOK { get; set; }
        public string Location { get; set; }
    }
}