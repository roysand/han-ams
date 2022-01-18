﻿using System;

namespace Application.Common.Models
{
    public class DailyTotalVm
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }

        public DailyTotalVm()
        {
        }
    }
}