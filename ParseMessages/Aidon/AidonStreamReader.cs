using System;
using System.IO;
using ParseMessages.Contracts;

namespace ParseMessages.Aidon
{
    public class AidonStreamReader : IStreamReader
    {
        private Stream _stream;
        private BinaryReader _reader;
        private long _length;
        private long _bytesRead;

        const byte NULL_BYTE = 0x0;
        readonly byte[] NULL_BYTES = { NULL_BYTE, NULL_BYTE };
        readonly byte[] INIT_BYTES = { 0x1, 0x1 };

        public AidonStreamReader(Stream stream) 
        {
            _stream = stream;
            _reader = new BinaryReader(_stream);
            _length = _stream.Length;
            _bytesRead = 0;
        }

        public bool EOF()
        {
            return (Position() >= _length);
        }

        /// <summary>
        /// Reads next byte from stream. No checking of content read.
        /// </summary>
        /// <returns>Byte Read</returns>
        public byte ReadByte()
        {
            byte b = NULL_BYTE;

            if (PossibleToReadBytes(1))
            {
                b = _reader.ReadByte();
                _bytesRead++;
            }

            return b;
        }

        /// <summary>
        /// Reads bnext n bytesToRead from stream. No checking of content
        /// </summary>
        /// <param name="bytesToRead"></param>
        /// <returns>Bytes read</returns>
        public byte[] ReadBytes(int bytesToRead)
        {
            byte[] b = null;

            if (!PossibleToReadBytes(bytesToRead))
            {
                b = _reader.ReadBytes(bytesToRead);
                _bytesRead++;
            }
            return b;
        }

        /// <summary>
        /// Read next hex byte from strem ex. 0x7F
        /// </summary>
        /// <returns>Next read hex byte, other wise empty stream  return byte[2] = {0x0,0x0}</returns>
        public byte[] ReadHexByte()
        {
            byte[] b = INIT_BYTES;

            b[0] = GetNextValidByte();

            if (b[0] == NULL_BYTE)
            {
                return NULL_BYTES;
            }

            b[1] = GetNextValidByte();
            if (b[1] == NULL_BYTE)
            {
                return NULL_BYTES;
            }


            return b;
        }

        private byte GetNextValidByte()
        {
            byte b = 0x1;

            while (!IsByteValid(b) && b != NULL_BYTE)
            {
                b = ReadByte();
            }

            return b;
        }

        public long Length()
        {
            return _length;
        }

        public long Position()
        {
            return _stream.Position;
        }

        private bool PossibleToReadBytes(int bytesToRead)
        {
            return ((Position() + bytesToRead) <= _stream.Length);
        }

        private Func<byte, bool> IsByteValid =
            c => (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F');
    }
}
