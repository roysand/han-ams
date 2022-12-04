using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Common.CommandAndQueries.ConsumerPrices.Queries.GetConsumerPrices
{
    public class GetConsumerPricesQuery : IRequest<GetConsumerPricesVm>
    {
        public GetConsumerPricesDto Dto { get; set; }

        public GetConsumerPricesQuery(GetConsumerPricesDto dto)
        {
            Dto = dto;
        }
    }
    
    public class GetConsumerPricesHandler : IRequestHandler<GetConsumerPricesQuery, GetConsumerPricesVm>
    {
        private readonly IPriceRepository<Price> _priceRepository;

        public GetConsumerPricesHandler(IPriceRepository<Price> priceRepository)
        {
            _priceRepository = priceRepository;
        }
        
        public async Task<GetConsumerPricesVm> Handle(GetConsumerPricesQuery request, CancellationToken cancellationToken)
        {
            return new GetConsumerPricesVm();
        }
    }
}