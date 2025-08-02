using System.Xml.Serialization;

namespace ExchangeRateComparison.Domain.Models;

[XmlRoot("Exchange")]
public class ExchangeXml
{
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}