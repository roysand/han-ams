using System;
using ParseMessages.Contracts;

namespace ParseMessages.Entities
{
    public class AidonMessageDto : IBaseMessage
    {
        byte _MessageFormat = 0x0;
        public AidonMessageDto()
        {
        }

        public int FrameFormat { get; set; }
        public int Length { get; set; }
        public int ClientAddress { get; set; }
        public int ServerAddress { get; set; }
        public int Control { get; set; }
        public int HCS { get; set; }
        public string LLC { get; set; }
    }
}
