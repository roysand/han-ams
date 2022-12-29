using System;
using System.Collections.Generic;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Config;

public class Config : IConfig
{
    private readonly IConfiguration _configuration;
    public IMqttConfig MqttConfig { get; }
    public IApplicationSettingsConfig ApplicationSettingsConfig { get; }

    public Config(IConfiguration configuration)
    {
        _configuration = configuration;
        
        MqttConfig = new MqttConfig(this);
        ApplicationSettingsConfig = new ApplicationSettingsConfig(this);
    }
    public T GetConfigValue<T>(string configKey, bool mustExist = false)
    {
        try
        {
            T configValue = _configuration.GetValue<T>(configKey);
            if (EqualityComparer<T>.Default.Equals(configValue, default(T)) && mustExist)
            {
                throw new ConfigException($"Config value '{configKey}' is missing");    
            }
            
            return configValue;
        }
        catch (Exception)
        {
            if (mustExist)
            {
                throw new ConfigException($"Config value '{configKey}' is missing");
            }
            else
            {
                return default(T);
            }
        }
    }
}