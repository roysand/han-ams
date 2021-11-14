using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using MBusReader.Contracts;

namespace MBusReader.Code
{
    public enum STATUS
    {
        Unknown = 0,
        Searching,
        Data
    }
    
    public class MBusReader : IMBusReader
    {
        private Stream _stream = null;
        private BinaryWriter _bw = null;
        private ISettingsSerial _settingsSerial = null;
        private SerialPort _serialPort;
        private STATUS _status = STATUS.Unknown;
        private List<byte> message = new List<byte>();
        private int _counter = 0;
        private long _dataCounter = 0;

        public MBusReader()
        {
            Init();
        }
        public MBusReader(Stream stream)
        {
            _stream = stream;
            Init();
        }

        public MBusReader(ISettingsSerial settingsSerial)
        {
            _settingsSerial = settingsSerial;
            Init();
        }

        public MBusReader(Stream stream, ISettingsSerial settingsSerial)
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
            
            _serialPort = new SerialPort(_settingsSerial.PortName, _settingsSerial.BaudRate, _settingsSerial.Parity,
                _settingsSerial.DataBits);
            _serialPort.Open();

            // _stream = _serialPort.BaseStream;
        }
        
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var serialPort = (SerialPort) sender;
            var byte2Read = serialPort.BytesToRead;
            var data = new byte[byte2Read];

            _counter++;
            serialPort.Read(data, 0, data.Length);
            _dataCounter += byte2Read;
            
            if ((_counter % 1000) == 0)
            {
                Console.WriteLine($"Data coming from MBus ({_dataCounter})");
            }
            

            foreach (var b in data)
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
                    if (message.Count == 1)
                    {
                        message.Clear();
                        message.Add(b);
                    }
                    else
                    {
                        // End of message
                        _status = STATUS.Searching;
                        message.Add(b);
                        // Console.WriteLine($"{b.ToString("X2")}");
                        Console.WriteLine($"Message length: {message.Count}");
                        message.ForEach(item => Console.Write($"{item.ToString("X2")} "));
                        Console.WriteLine();

                        if (_bw != null)
                        {
                            message.ForEach(item => _bw.Write(item));
                        }
                    }
                }
                else if (_status == STATUS.Data)
                {
                    // Inside a message
                    message.Add(b);
                    // Console.Write($"{b.ToString("X2")} ");
                }
                else
                {
                    Console.WriteLine($"Waiting for package start");
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
                _serialPort.Close();
            }

            return true;
        }

        public void Run(bool printToScreen)
        {
            Console.WriteLine("Starting reading data ...!");

            _serialPort.DataReceived  += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose ...!");
            _stream?.Dispose();
            _serialPort?.Dispose();
        }
    }
}
