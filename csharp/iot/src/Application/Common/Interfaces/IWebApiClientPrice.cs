using System.Net.Http;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IWebApiClientPrice : IWebApiClient
    {
        Task<HttpResponseMessage> GetPriceDayAhead();
    }
}