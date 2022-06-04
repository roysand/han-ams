using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Clients
{
    public class WebApiClientPrice : HttpClient, IWebApiClientPrice
    {
        private readonly IConfiguration _configuration;

        public WebApiClientPrice(IConfiguration configuration)
        {
            _configuration = configuration;
            var Url1 =
                "https://transparency.entsoe.eu/api?documentType=A44&in_Domain=10YNO-2--------T&out_Domain=10YNO-2--------T&periodStart=202205310000&periodEnd=202205312300&securityToken=6f932556-996f-45e9-b73a-4cb0159ef564";
            Url =
                "https://transparency.entsoe.eu/api?documentType=A44&in_Domain=10YNO-2--------T&out_Domain=10YNO-2--------T&periodStart={0}&periodEnd={1}&securityToken=6f932556-996f-45e9-b73a-4cb0159ef564";
        }

        public string Url { get; set; }
        public async Task<HttpResponseMessage> GetPriceDayAhead()
        {
            Url = string.Format(Url,
                DateTime.Now.Date.AddDays(1).ToString("yyyyMMdd" + "0000"), DateTime.Now.Date.AddDays(1).ToString("yyyyMMdd" + "2300"));
            
            var result = await this.GetAsync(Url);
            return result;
        }
    }
}