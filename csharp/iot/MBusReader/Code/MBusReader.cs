﻿using MBusReader.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using ExtendedSerialPort;

namespace MBusReader.Code
{
    public enum STATUS
    {
        Unknown = 0,
        Searching,
        Data
    }
    public class MBusReader : IMBusReader, IDisposable
    {
        private Stream _stream;
        private ISettingsSerial _settingsSerial = null;
        private ReliableSerialPort _serialPort;
        private STATUS _status = STATUS.Unknown;
        private List<byte> message = new List<byte>();

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
                    
                    Console.Write($"{b.ToString("X2")} ");
                }
                else if ((b == 0x7E) && _status == STATUS.Data)
                {
                    // End of message
                    _status = STATUS.Searching;
                    message.Add(b);
                    Console.WriteLine($"{b.ToString("X2")}");
                    Console.WriteLine($"Message length: {message.Count}");
                    message.ForEach(item => Console.Write(item.ToString("X2 ")));
                    Console.WriteLine();
                }
                else if (_status == STATUS.Data)
                {
                    // Inside a message
                    message.Add(b);
                    Console.Write($"{b.ToString("X2")} ");
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
    }
}
