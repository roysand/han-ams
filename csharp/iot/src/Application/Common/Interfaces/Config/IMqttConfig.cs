namespace Application.Common.Interfaces.Config;

public interface IMqttConfig
{
    string MQTTTopic();
    string MQTTServerURI();
    int MQTTServerPortNr();
    string MQTTUserName();
    string MQTTUserPassword();
    bool MQTTUseTLS();
}