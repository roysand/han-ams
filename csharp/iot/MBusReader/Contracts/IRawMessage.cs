using System;

namespace MBusReader.Contracts
{
    public interface IRawMessage
    {
        DateTime TimeStamp { get; set; }
        Guid Id { get; set; }
        string Location { get; set; }
        string Raw { get; set; }
    }
}