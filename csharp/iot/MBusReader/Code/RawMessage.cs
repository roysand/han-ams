using System;
using MBusReader.Contracts;

namespace MBusReader.Code
{
    public class RawMessage : IRawMessage
    {
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Location { get; set; }
        public string Raw { get; set; }
    }
}