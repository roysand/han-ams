using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Application.Common.Models
{
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ExchangeRateNB
    {
        public List<int> Attributes { get; set; }
        public Observations Observations { get; set; }
    }

    public class Attributes
    {
        public List<object> Dataset { get; set; }
        public List<Series> Series { get; set; }
        public List<object> Observation { get; set; }
    }

    public class Data
    {
        public List<DataSet> DataSets { get; set; }
        public Structure Structure { get; set; }
    }

    public class DataSet
    {
        public List<Link> Links { get; set; }
        public string Action { get; set; }
        public Series Series { get; set; }
    }

    public class Descriptions
    {
        public string En { get; set; }
    }

    public class Dimensions
    {
        public List<object> Dataset { get; set; }
        public List<Series> Series { get; set; }
        public List<Observation> Observation { get; set; }
    }

    public class Link
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Uri { get; set; }
        public string Urn { get; set; }
    }

    public class Meta
    {
        public string Id { get; set; }
        public DateTime Prepared { get; set; }
        public bool Test { get; set; }
        public Sender Sender { get; set; }
        public Receiver Receiver { get; set; }
        public List<Link> Links { get; set; }
    }

    public class Names
    {
        public string En { get; set; }
    }

    public class Observation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int KeyPosition { get; set; }
        public string Role { get; set; }
        public List<Value> Values { get; set; }
    }

    public class Observations
    {
        public List<string> _0 { get; set; }
    }

    public class Receiver
    {
        public string Id { get; set; }
    }

    public class Relationship
    {
        public List<string> Dimensions { get; set; }
    }

    public class Root
    {
        public Meta Meta { get; set; }
        public Data Data { get; set; }
    }

    public class Sender
    {
        public string Id { get; set; }
    }

    public class Series
    {
        [JsonProperty("0:0:0:0")]
        public ExchangeRateNB ExchangeRateNb { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int KeyPosition { get; set; }
        public object Role { get; set; }
        public List<Value> Values { get; set; }
        public Relationship Relationship { get; set; }
    }

    public class Structure
    {
        public List<Link> Links { get; set; }
        public string Name { get; set; }
        public Names Names { get; set; }
        public string Description { get; set; }
        public Descriptions Descriptions { get; set; }
        public Dimensions Dimensions { get; set; }
        public Attributes Attributes { get; set; }
    }

    public class Value
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }


}