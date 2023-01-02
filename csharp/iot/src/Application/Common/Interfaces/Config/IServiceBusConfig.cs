namespace Application.Common.Interfaces.Config;

public interface IServiceBusConfig
{
    string SBTopic();
    string SBSubscription();
    string SBConnection();
    int SBMaxMessagesToRead();
}