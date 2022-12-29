using System.Data.Common;

namespace Application.Common.Interfaces;

public interface IServiceBusConfig
{
    string SBTopic();
    string SBSubscription();
    string SBConnection();
    int SBMaxMessagesToRead();
}