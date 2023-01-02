using System.Threading.Tasks;
using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IMqttManagedClient
    {
        Task SubscribeAsync(string topic, int qos = 1);
        Task UnSubscribeAsync(string topic);
        Task Disconnect();
        Task Save(AMSReaderData data);
    }
}