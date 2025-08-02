using System.Xml.Serialization;

namespace ExchangeRateComparison.Domain.Models;

[XmlRoot("Result")]
public class ResultXml
{
    [XmlText]
    public decimal Result { get; set; }
}