using System.Reflection;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Config;

namespace Infrastructure.Config;

public class MqttConfig : IMqttConfig
{
    private readonly IConfig _config;
    private readonly string ConfigParentKey = "MQTT";

    public MqttConfig(IConfig config)
    {
        _config = config;
    }
    
    public string MQTTTopic()
    {
        return _config.GetConfigValue<string>($"{ConfigParentKey}:{MethodBase.GetCurrentMethod()!.Name}");
    }

    public string MQTTServerURI()
    {
        return _config.GetConfigValue<string>($"{ConfigParentKey}:{MethodBase.GetCurrentMethod()!.Name}");
    }

    public int MQTTServerPortNr()
    {
        return _config.GetConfigValue<int>($"{ConfigParentKey}:{MethodBase.GetCurrentMethod()!.Name}");
    }

    public string MQTTUserName()
    {
        return _config.GetConfigValue<string>($"{ConfigParentKey}:{MethodBase.GetCurrentMethod()!.Name}");
    }

    public string MQTTUserPassword()
    {
        return _config.GetConfigValue<string>($"{ConfigParentKey}:{MethodBase.GetCurrentMethod()!.Name}");
    }

    public bool MQTTUseTLS()
    {
        return _config.GetConfigValue<bool>($"{ConfigParentKey}:{MethodBase.GetCurrentMethod()!.Name}");
    }
}