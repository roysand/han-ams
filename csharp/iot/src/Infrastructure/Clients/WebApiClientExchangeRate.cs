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
        public WebApiClientExchangeRate(IConfiguration configuration)
        {
            this.UrlBase = configuration["ExchangeRateUrl"];
            QueryParam = new System.Collections.Specialized.NameValueCollection()
            {
                {"startPeriod", "2022-01-01"},
                {"endPeriod", "2022-01-01"},
            };
        }
        
        public string UrlBase { get; set; }
        public System.Collections.Specialized.NameValueCollection QueryParam { get; set; }
        
        public async Task<ICollection<ExchengeRate>> DownloadExchangeRates(DateTime startDate, DateTime? endDate)
        {
            HttpResponseMessage responseMessage = null;
            var result = new List<ExchengeRate>();

            endDate ??= DateTime.Now.AddDays(1);

            var deltaDays = Math.Min(365, (endDate.Value  - startDate).Days);
            
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
                int missingExchangeRatesCount = 0;
                missingExchangeRatesCount = (int)((result[i + 1].ExchangeRatePeriod - result[i].ExchangeRatePeriod).TotalDays - 1);
                
                if (missingExchangeRatesCount > 1)
                {
                    missingDates.AddRange(CreateMissingExchangeRates(result[i], missingExchangeRatesCount));
                    // missingDates.Add(new ExchengeRate()
                    // {
                    //     ExchangeRatePeriod = result[i].ExchangeRatePeriod.AddDays(1),
                    //     ExchangeRate = result[i].ExchangeRate,
                    //     ExchangeRateType = result[i].ExchangeRateType
                    // });
                }
            }

            result.AddRange(missingDates);
            return result;
        }

        private List<ExchengeRate> CreateMissingExchangeRates(ExchengeRate exchangeRateMaster, int count)
        {
            var missingExchangeRates = new List<ExchengeRate>();

            for (int i = 0; i < count; i++)
            {
                missingExchangeRates.Add( new ExchengeRate()
                {
                    ExchangeRatePeriod = exchangeRateMaster.ExchangeRatePeriod.AddDays(i + 1),
                    ExchangeRate = exchangeRateMaster.ExchangeRate,
                    ExchangeRateType = exchangeRateMaster.ExchangeRateType
                });
            }
            
            return missingExchangeRates;
        }
    }
}