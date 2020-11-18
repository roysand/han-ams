using System;
namespace ParseMessages.Contracts
{
    public interface IStreamReader
    {
        byte ReadByte();
        byte[] ReadBytes(int bytesToRead);
        byte[] ReadHexByte();
        bool EOF();
        long Length();
        long Position();
    }
}
