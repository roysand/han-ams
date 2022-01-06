using System.Threading.Tasks;

namespace MessageService.Contracts
{
    public interface IMessagePublisher
    {
        Task Publish<T>(T obj);
        Task Publish(string raw);
    }
}