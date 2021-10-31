﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MBusReader.Contracts
{
    public interface IHDLCMessage
    {
        IHDLCHeader Header { get; set; }
        IList<IHDLCData> Data { get; set; }
    }

    public interface IHDLCHeader
    {
        string Hdlc_Length { get; set; }
        
    }

    public interface IHDLCData
    {
        string Name { get; set; }
        string Description { get; set; }
        string Obis_Code { get; set; }
        string Unit { get; set; }
        float Value { get; set; }
    }
}
