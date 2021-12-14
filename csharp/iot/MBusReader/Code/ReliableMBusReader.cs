using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        private bool PrintToScreen = false;

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
                    
                    // Console.WriteLine($"{b.ToString("X2")}");
                    if (PrintToScreen)
                    {
                        message.ForEach(item => Console.Write($"{item.ToString("X2")} "));
                    }

                    if (_bw != null)
                    {
                        message.ForEach(item => _bw.Write(item));
                    }

                    IHDLCMessage hdlcMessage = new HDLCMessage();
                    var parser = new Parser();
                    hdlcMessage = parser.Parse(message.Skip(1).Take(message.Count-1).ToList());
                    
                    if (PrintToScreen && hdlcMessage.Data.Count > 0)
                    {
                        Console.Write($"Epoc {hdlcMessage.Header.SecondsSinceEpoc}");
                        foreach (var data in hdlcMessage.Data)
                        {
                            Console.WriteLine($" Name = {data.Name} Value={data.Value} {data.Unit}");
                        }
                    }
                }
                else if (_status == STATUS.Data)
                {
                    // Inside a message
                    message.Add(b);
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
            }

            return true;
        }

        public void Run(bool printToScreen)
        {
            PrintToScreen = printToScreen;
            _serialPort.DataReady += DataReceivedHandler;
        }

        public void Dispose()
        {
            _stream?.Dispose();
            _serialPort?.Dispose();
        }
        
        // private string ConvertToEpocHexString(DateTime dateTime)
        // {
        //     TimeSpan t = dateTime- new DateTime(1970, 1, 1);
        //     int secondsSinceEpoch = (int)t.TotalSeconds;
        //       
        //     byte[] epocByte = BitConverter.GetBytes(secondsSinceEpoch);
        //     if (BitConverter.IsLittleEndian)
        //         epocByte = epocByte.Reverse().ToArray();
        //     var epocString = BitConverter.ToString(epocByte).Replace("-","");
        //
        //     return epocString;
        // }
    }
}
