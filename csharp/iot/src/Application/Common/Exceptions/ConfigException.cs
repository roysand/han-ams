using System;

namespace Application.Common.Exceptions;

public class ConfigException : Exception
{
    public ConfigException(string message)
        : base(message)
    {

    }
}