using System.Xml.Serialization;

namespace ExchangeRateComparison.Domain.Models;

[XmlRoot("Result")]
public class ExchangeResultAttributes
{
    [XmlAttribute("Rate")]
    public decimal Rate { get; set; }

    [XmlAttribute("Total")]
    public decimal Total { get; set; }
}