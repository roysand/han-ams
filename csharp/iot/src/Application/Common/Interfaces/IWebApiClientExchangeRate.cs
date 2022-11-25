using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IWebApiClientExchangeRate : IWebApiClient
    {
        Task<ICollection<ExchengeRate>> DownloadExchangeRates();
    }
}