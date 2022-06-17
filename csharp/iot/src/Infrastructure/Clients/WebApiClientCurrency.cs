using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Clients
{
    public class WebApiClientCurrency : HttpClient, IWebApiClientCurrency
    {
        private readonly IConfiguration _configuration;
        private readonly ICurrencyRepository<Currency> _currencyRepository;

        public WebApiClientCurrency(IConfiguration configuration, ICurrencyRepository<Currency> currencyRepository)
        {
            _configuration = configuration;
            _currencyRepository = currencyRepository;
            this.UrlBase = _configuration["CurrencyUrl"];
        }
        
        public string UrlBase { get; set; }
        public async Task<Currency> GetCurrency(DateTime date)
        {
            var currency = await _currencyRepository.FindAsync(date);
            return currency;
        }
    }
}