using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ParseMessages.Entities;

namespace ParseMessages.Contracts
{
    public interface IMessageParser
    {
        IList<AidonMessageDto> Run(Stream stream);
        IBaseMessage Parse(StringBuilder message);
    }
}
