using System;
using System.Collections.Generic;
using System.Text;

namespace MBusReader.Contracts
{
    public interface IMBusReader : IDisposable
    {
        void Run();
        Byte[] Pull();
        bool Close();
    }
}
