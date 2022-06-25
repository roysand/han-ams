using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Clients
{
    public class WebApiClientExchangeRate : HttpClient, IWebApiClientExchangeRate
    {
        private readonly IConfiguration _configuration;
        private readonly IExchangeRateRepository<ExchengeRate> _exchangeRateRepository;

        public WebApiClientExchangeRate(IConfiguration configuration, IExchangeRateRepository<ExchengeRate> exchangeRateRepository)
        {
            _configuration = configuration;
            _exchangeRateRepository = exchangeRateRepository;
            this.UrlBase = _configuration["ExchangeRateUrl"];
        }
        
        public string UrlBase { get; set; }
        public async Task<ICollection<ExchengeRate>> DownloadExchangeRates()
        {
            var startDate = DateTime.Now;
            HttpResponseMessage responseMessage = null;

            var latestExchangeRate = await _exchangeRateRepository.FindNewestAsync();
            if (latestExchangeRate == null)
            {
                startDate = new DateTime(2022, 1, 1);
            }
            else
            {
                startDate = latestExchangeRate.ExchangeRatePeriod.AddDays(1);
            }

            var deltaDays = Math.Min(365, (DateTime.Now.AddDays(1) - startDate).Days);
            responseMessage = await this.GetAsync(UrlBase);
            
            return null;
        }
    }
}