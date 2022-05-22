using System.Net.Http;
using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Common.Helpers
{
    public class WebApiClientPrice : HttpClient, IWebApiClientPrice
    {
        private readonly IConfiguration _configuration;

        public WebApiClientPrice(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        
    }
}