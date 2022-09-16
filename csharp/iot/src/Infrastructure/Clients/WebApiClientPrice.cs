using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;

namespace Infrastructure.Clients
{
    public class WebApiClientPrice : HttpClient, IWebApiClientPrice
    {
        private readonly IConfiguration _configuration;
        private readonly IPriceRepository<Price> _priceRepository;

        public WebApiClientPrice(IConfiguration configuration, IPriceRepository<Price> priceRepository)
        {
            _configuration = configuration;
            _priceRepository = priceRepository;
            UrlBase = _configuration["DayAHeadUrl"];
            InDomain = _configuration["InDomain"];
            OutDomain = _configuration["OutDomain"];
            QueryParam = new System.Collections.Specialized.NameValueCollection()
            {
                { "in_domain", InDomain},
                {"out_domain", OutDomain},
                {"periodStart", "2022010100"},
                {"periodEnd", "202201012300"},
                {"securityToken","6f932556-996f-45e9-b73a-4cb0159ef564"}
            };
        }

        public string UrlBase { get; set; }
        public string InDomain { get; set; }
        public string OutDomain { get; set; }
        public System.Collections.Specialized.NameValueCollection QueryParam { get; set; }

        public async Task<ICollection<Price>> GetPriceDayAhead()
        {
            var startDate = DateTime.Now;
            var result = new List<Price>();
            HttpResponseMessage responseMessage = null;
            string content = string.Empty;
            Publication_MarketDocument parsedResult = null;
            Price price = null;
            string url = string.Empty;
            
            var lastPrice = _priceRepository.FindMaxPricePeriod();
            if (lastPrice == null)
            {
                startDate = new DateTime(2022, 1, 1);
            }
            else
            {
                startDate = lastPrice.PricePeriod.AddDays(1);
            }
            
            if ((DateTime.Now.Hour > 13) && (DateTime.Now.Date > startDate.Date))
            { 
                var deltaDays = Math.Min(365, (DateTime.Now.AddDays(1) - startDate).Days);
                for (int i = 0; i <= deltaDays; i++)
                {
                    QueryParam.Set("periodStart", startDate.AddDays(i).ToString("yyyyMMdd" + "0000"));
                    QueryParam.Set("periodEnd", startDate.AddDays(i).ToString("yyyyMMdd" + "2300"));
                    url = HttpParams.Add(UrlBase, QueryParam);

                   try
                    {
                        responseMessage = await this.GetAsync(url);
                        content = await responseMessage.Content.ReadAsStringAsync();

                        using (TextReader reader = new StringReader(content))
                        {
                            var serializer = new XmlSerializer(typeof(Publication_MarketDocument));
                            parsedResult = (Publication_MarketDocument)serializer.Deserialize(reader);
                        }
                    
                        price = parsedResult.CreatePriceDetail();
                        result.Add(price);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }

            return result;
        }
    }
}