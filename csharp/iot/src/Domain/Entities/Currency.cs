using System;

namespace Domain.Entities
{
    public enum CurrencyTypes
    {
        EUR = 1
    }
    
    public class Currency
    {
        public Guid CurrencyId { get; set; }
        public DateTime CurrencyPeriod { get; set; }
        public CurrencyTypes CurrencyType { get; set; }
        public decimal ExchangeRate { get; set; }

        public Currency()
        {
            CurrencyType = CurrencyTypes.EUR;
        }
    }
}