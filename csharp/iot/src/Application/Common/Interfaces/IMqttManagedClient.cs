using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IMqttManagedClient
    {
        Task SubscribeAsync(string topic, int qos = 1);
        Task UnSubscribeAsync(string topic);
        Task Disconnect();
    }
}