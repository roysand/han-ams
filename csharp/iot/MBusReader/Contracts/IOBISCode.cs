namespace MBusReader.Contracts
{
    public interface IOBISCode
    {
        string ObisCode { get; set; }
        string ObjectCode { get; set; }
        string Unit { get; set; }
        string Name { get; set; }
        int Scaler { get; set; }
        int Size { get; set; }
        string DataTypeName { get; set; }
    }
}