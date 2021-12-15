using MBusReader.Contracts;

namespace MBusReader.Code
{
    public class OBISCode : IOBISCode
    {
        private string _obisCode;
        private string _dataCode;
        private string _unit;
        private string _name;
        private int _scaler;

        public string ObisCode
        {
            get => _obisCode;
            set => _obisCode = value;
        }

        public string ObjectCode
        {
            get => _dataCode;
            set => _dataCode = value;
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
    }
}