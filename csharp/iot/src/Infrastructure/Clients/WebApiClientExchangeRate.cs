using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Models;
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
            QueryParam = new System.Collections.Specialized.NameValueCollection()
            {
                {"startPeriod", "2022-01-01"},
                {"endPeriod", "2022-01-01"},
            };
        }
        
        public string UrlBase { get; set; }
        public System.Collections.Specialized.NameValueCollection QueryParam { get; set; }
        
        public async Task<ICollection<ExchengeRate>> DownloadExchangeRates(DateTime? start)
        {
            var startDate = DateTime.Now;

            HttpResponseMessage responseMessage = null;
            var result = new List<ExchengeRate>();

            if (start == null)
            {
                ExchengeRate latestExchangeRate = await _exchangeRateRepository.FindNewestAsync();
                if (latestExchangeRate != null)
                {
                    startDate = latestExchangeRate.ExchangeRatePeriod.AddDays(1);
                }
            }
            else
            {
                startDate = start.Value;
            }

            var deltaDays = Math.Min(365, ((end ?? startDate.AddDays(1)) - startDate).Days);
            
            QueryParam.Set("startPeriod", startDate.ToString("yyyy-MM-dd"));
            QueryParam.Set("endPeriod", startDate.AddDays(deltaDays).ToString("yyyy-MM-dd"));
            var url = HttpParams.Add(UrlBase, QueryParam);
            
            responseMessage = await this.GetAsync(url);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return result;
            }
            
            var content = await responseMessage.Content.ReadAsStringAsync();
            
            GenericData parsedResult = null;
            
            try
            {
                var serializer = new XmlSerializer(typeof(GenericData));
                
                using (TextReader reader = new StringReader(content))
                {
                    parsedResult = (GenericData)serializer.Deserialize(reader);
                }

                foreach (var obs in parsedResult?.DataSet.Series.Obs)
                {
                    var exchangeRate = new ExchengeRate()
                    {
                        ExchangeRatePeriod = obs.ObsDimension.value,
                        ExchangeRate = obs.ObsValue.value
                    };

                    result.Add(exchangeRate);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            var missingDates = new List<ExchengeRate>();

            // Norges bank does not calculate exchange rates every day!!
            // Create missing exchange rates
            for(var i = 0; i < result.Count - 1; i++)
            {
                if ((result[i + 1].ExchangeRatePeriod - result[i].ExchangeRatePeriod).TotalDays > 1)
                {
                    missingDates.Add(new ExchengeRate()
                    {
                        ExchangeRatePeriod = result[i].ExchangeRatePeriod.AddDays(1),
                        ExchangeRate = result[i].ExchangeRate,
                        ExchangeRateType = result[i].ExchangeRateType
                    });
                }
            }

            result.AddRange(missingDates);
            return result;
        }
    }
}