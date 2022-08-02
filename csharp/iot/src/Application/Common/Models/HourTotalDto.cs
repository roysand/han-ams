using System;
using System.Collections.Generic;

namespace Application.Common.Models
{
    public class HourTotalDto
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        
        public HourTotalDto()
        {
        }
    }
}