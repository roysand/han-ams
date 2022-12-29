using Newtonsoft.Json;

namespace Application.Common.Models;

public class AmsReaderData
{
    [JsonProperty("lv")] public string Lv { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("type")] public string Type { get; set; }

    [JsonProperty("P")] public int P { get; set; }

    [JsonProperty("Q")] public int Q { get; set; }

    [JsonProperty("PO")] public int PO { get; set; }

    [JsonProperty("QO")] public int QO { get; set; }

    [JsonProperty("I1")] public double I1 { get; set; }

    [JsonProperty("I2")] public double I2 { get; set; }

    [JsonProperty("I3")] public double I3 { get; set; }

    [JsonProperty("U1")] public double U1 { get; set; }

    [JsonProperty("U2")] public double U2 { get; set; }

    [JsonProperty("U3")] public double U3 { get; set; }
}
 