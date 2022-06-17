using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IWebApiClientCurrency : IWebApiClient
    {
        Task<Currency> GetCurrency(DateTime date);
    }
}