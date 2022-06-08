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
            UrlOrig = _configuration["DayAHeadUrl"];
            // "https://transparency.entsoe.eu/api?documentType=A44&in_Domain=10YNO-2--------T&out_Domain=10YNO-2--------T&periodStart={0}&periodEnd={1}&securityToken=6f932556-996f-45e9-b73a-4cb0159ef564";
        }

        public string UrlOrig { get; set; }

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
            
            if ((DateTime.Now.Hour > 13) || (DateTime.Now - startDate).Days > 1) // && DateTime.Now.Minute > 15)
            { 
                var deltaDays = Math.Min(30, (DateTime.Now.AddDays(1) - startDate).Days);
                for (int i = 0; i <= deltaDays; i++)
                {
                    url = string.Format(UrlOrig,
                        startDate.AddDays(i).ToString("yyyyMMdd" + "0000"), startDate.AddDays(i).ToString("yyyyMMdd" + "2300"));

                    responseMessage = await this.GetAsync(url);
                    content = await responseMessage.Content.ReadAsStringAsync();

                    try
                    {
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