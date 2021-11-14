using System;
using System.Collections.Generic;
using System.Text;

namespace MBusReader.Contracts
{
    public interface IMBusReader : IDisposable
    {
        void Run(bool printToScreen);
        Byte[] Pull();
        bool Close();
    }
}
