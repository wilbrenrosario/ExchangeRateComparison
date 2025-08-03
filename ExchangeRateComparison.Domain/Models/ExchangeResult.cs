using System.Xml.Serialization;

namespace ExchangeRateComparison.Domain.Models;

[XmlRoot("ExchangeResult")]
public class ExchangeResult
{
    [XmlElement("Rate")]
    public decimal Rate { get; set; }

    [XmlElement("Total")]
    public decimal Total { get; set; }
}