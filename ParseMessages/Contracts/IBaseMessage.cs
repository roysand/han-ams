using System;
namespace ParseMessages.Contracts
{
    public interface IBaseMessage
    {
        int Length { get; set; }
        int FrameFormat { get; set; }
        int ClientAddress { get; set; }
        int ServerAddress { get; set; }
        int Control { get; set; }
        int HCS { get; set; }
        string LLC { get; set; }
    }
}
