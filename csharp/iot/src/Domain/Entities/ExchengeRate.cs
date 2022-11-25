using System;

namespace Domain.Entities
{
    public enum ExchangeRateTypes
    {
        EUR = 1
    }
    
    public class ExchengeRate
    {
        public Guid ExchangeRateId { get;  }
        public DateTime ExchangeRatePeriod { get; set; }
        public ExchangeRateTypes ExchangeRateType { get; set; }
        public decimal ExchangeRate { get; set; }

        public ExchengeRate()
        {
            ExchangeRateType = ExchangeRateTypes.EUR;
            ExchangeRateId = Guid.NewGuid();
        }
    }
}