using System;

namespace Domain.Entities
{
    public class PriceDetail
    {
        public Guid Id { get; set; }
        public Guid PricePK { get; set; }
        public DateTime PricePeriod { get; set; }
        public decimal Price { get; set; }

        public PriceDetail()
        {
            
        }
    }
}
