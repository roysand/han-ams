using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExtendedSerialPort;
using MBusReader.Contracts;
using MessageParser;
using MessageParser.Code;

namespace MBusReader.Code
{
    public class ReliableMBusReader : IMBusReader, IDisposable
    {
        private Stream _stream = null;
        private BinaryWriter _bw = null;
        private ISettingsSerial _settingsSerial = null;
        private ReliableSerialPort _serialPort;
        private STATUS _status = STATUS.Unknown;
        private List<byte> message = new List<byte>();

        public ReliableMBusReader()
        {
            Init();
        }
        public ReliableMBusReader(Stream stream)
        {
            _stream = stream;

            Init();
        }

        public ReliableMBusReader(ISettingsSerial settingsSerial)
        {
            _settingsSerial = settingsSerial;
            Init();
        }

        public ReliableMBusReader(Stream stream, ISettingsSerial settingsSerial)
        {
            _stream = stream;
            _settingsSerial = settingsSerial;
            Init();
        }

        private void Init()
        {
            _status = STATUS.Searching;
            if (_stream != null)
            {
                Console.WriteLine("Write to file");
                _bw = new BinaryWriter(_stream);
            }
            
            if (_settingsSerial == null)
            {
                _settingsSerial = new SettingsSerial();
            }
            
            _serialPort = new ReliableSerialPort(_settingsSerial.PortName, _settingsSerial.BaudRate, _settingsSerial.Parity,
                _settingsSerial.DataBits);
            _serialPort.Open();

            _stream = _serialPort.BaseStream;
        }

        // (delegate) void System.IO.Ports.SerialDataReceivedEventHandler(object sender, SerialDataReceivedEventArgs e
        private void DataReceivedHandler(object sender, DataReceivedArgs e)
        {
            // var serialPort = (SerialPort) sender;
            
            // serialPort.Read(data, 0, data.Length);

            foreach (var b in e.Data)
            {
                if ((b == 0x7E) && (_status == STATUS.Searching))
                {
                    // Beginning of a new message
                    _status = STATUS.Data;
                    message.Clear();
                    message.Add(b);
                    // Console.Write($"{b.ToString("X2")} ");
                }
                else if ((b == 0x7E) && _status == STATUS.Data && message.Count == 1 && message[0] == 0x7E)
                {
                    Console.WriteLine("Skipping end of frame");
                }
                else if ((b == 0x7E) && _status == STATUS.Data)
                {
                    // End of message
                    _status = STATUS.Searching;
                    message.Add(b);
                    
                    var epocTimeString = ConvertToEpocHexString(DateTime.Now);

                    // Console.WriteLine($"{b.ToString("X2")}");
                    Console.WriteLine($"{epocTimeString} Message length: {message.Count}");
                    message.ForEach(item => Console.Write($"{item.ToString("X2")} "));
                    Console.WriteLine();

                    if (_bw != null)
                    {
                        _bw.Write(epocTimeString);
                        message.ForEach(item => _bw.Write(item));
                    }

                    IHDLCMessage hdlcMessage = new HDLCMessage();
                    var parser = new Parser(hdlcMessage);
                    hdlcMessage = parser.Parse(message);
                }
                else if (_status == STATUS.Data)
                {
                    // Inside a message
                    message.Add(b);
                    // Console.Write($"{b.ToString("X2")} ");
                }
            }
        }

        public byte[] Pull()
        {
            throw new NotImplementedException();
        }
        
        public bool Close()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.DataReady -= DataReceivedHandler;
                // _serialPort.Close();
            }

            return true;
        }

        public void Run()
        {
            Console.WriteLine("Starting reading data ...!");

            _serialPort.DataReady += DataReceivedHandler;
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose ...!");
            _stream?.Dispose();
            _serialPort?.Dispose();
        }
        
        private string ConvertToEpocHexString(DateTime dateTime)
        {
            TimeSpan t = dateTime- new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
              
            byte[] epocByte = BitConverter.GetBytes(secondsSinceEpoch);
            if (BitConverter.IsLittleEndian)
                epocByte = epocByte.Reverse().ToArray();
            var epocString = BitConverter.ToString(epocByte).Replace("-","");

            return epocString;
        }
    }
}