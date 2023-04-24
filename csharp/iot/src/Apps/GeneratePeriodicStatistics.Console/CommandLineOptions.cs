using CommandLine;
using CommandLine.Text;

namespace GeneratePeriodicStatistics.Console;
[Flags]
public enum ServiceType
{
    Minute = 0x1,
    Hour = 0x2
}

public class CommandLineOptions
{
    [Value(0, MetaName = "service", Required = false, Default = ServiceType.Minute, HelpText = "The name of service to run ex. Minute, Hour.")]
    public ServiceType Service { get; set; }
    
    // Usage provide meta data for help screen.
    [Usage(ApplicationAlias = "forecasts")]
    public static IEnumerable<Example> Examples => new List<Example>
    {
        new Example("Update database with Periodic statistics",
            new CommandLineOptions { Service = ServiceType.Minute })
    };
}