namespace Application.Common.Interfaces.Config;

public interface IConfig
{
    IMqttConfig MqttConfig { get;  }
    IApplicationSettingsConfig ApplicationSettingsConfig { get;  }
    T GetConfigValue<T>(string configKey, bool mustExist = false);
}