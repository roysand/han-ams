using System;

namespace Application.Common.Models;

public class DayVm
{
    public DateTime TimeStamp { get; set; }
    public string Location { get; set; }
    public string Unit { get; set; }
    public decimal ValueNum { get; set; }
    public int Count { get; set; }
    public decimal PriceNOK { get; set; }
}