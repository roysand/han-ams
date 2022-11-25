using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IWebApiClientPrice : IWebApiClient
    {
        Task<ICollection<Price>> GetPriceDayAhead();
    }
}