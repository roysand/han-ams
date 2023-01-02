using System;
using Newtonsoft.Json;

namespace Application.Common.Models;

public class AMSReaderData
{
    public DateTime TimeStamp { get; } = DateTime.Now; 
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("up")]
    public int Up { get; set; }

    [JsonProperty("t")]
    public int T { get; set; }

    [JsonProperty("vcc")]
    public double Vcc { get; set; }

    [JsonProperty("rssi")]
    public int Rssi { get; set; }

    [JsonProperty("temp")]
    public double Temp { get; set; }

    [JsonProperty("data")]
    public AMSReaderDataDetail Data { get; set; }
}
 