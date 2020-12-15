using MBusReader.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;

namespace MBusReader.Code
{
    class MBusReader : IMBusReader, IDisposable
    {
        private Stream _stream;
        private  ISettingsSerial _settingsSerial = null;
        private SerialPort _serialPort;
        
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
            if (_settingsSerial == null)
            {
                _settingsSerial = new SettingsSerial();
            }

            _serialPort = new SerialPort(_settingsSerial.PortName, _settingsSerial.BaudRate, _settingsSerial.Parity,
                _settingsSerial.DataBits);
            _serialPort.Open();

            _stream = _serialPort.BaseStream;
            
            _serialPort.DataReceived  += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            
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

        public byte Read()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _stream?.Dispose();
            _serialPort?.Dispose();
        }
    }
}
