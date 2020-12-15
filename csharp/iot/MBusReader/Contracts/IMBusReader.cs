using System;
using System.Collections.Generic;
using System.Text;

namespace MBusReader.Contracts
{
    public interface IMBusReader
    {
        Byte Read();
        Byte[] Pull();
        bool Close();
    }
}
