using MBusReader.Contracts;

namespace MBusReader.Code
{
    public class OBISCode : IOBISCode
    {
        private string _obisCode;
        private string _objectCode;
        private string _unit;
        private string _name;
        private int _scaler;
        private int _size;
        private string _dataTypeName;

        public string ObisCode
        {
            get => _obisCode;
            set => _obisCode = value;
        }

        public string ObjectCode
        {
            get => _objectCode;
            set => _objectCode = value;
        }

        public string Unit
        {
            get => _unit;
            set => _unit = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int Scaler
        {
            get => _scaler;
            set => _scaler = value;
        }

        public int Size
        {
            get => _size;
            set => _size = value;
        }

        public string DataTypeName
        {
            get => _dataTypeName;
            set => _dataTypeName = value;
        }
    }
}