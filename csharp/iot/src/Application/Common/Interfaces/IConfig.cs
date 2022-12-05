namespace Application.Common.Interfaces;

public interface IConfig
{
    IMqttConfig MqttConfig { get;  }
    IApplicationSettingsConfig ApplicationSettingsConfig { get;  }
    T GetConfigValue<T>(string configKey, bool mustExist = false);
}