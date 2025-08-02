using System.Text;
using System.Xml.Serialization;
using ExchangeRateComparison.Domain;

namespace ExchangeRateComparison.Infrastructure.Providers;

public class Api2ExchangeProvider : IExchangeRateProvider
{
    private readonly HttpClient _http;

    public Api2ExchangeProvider(IHttpClientFactory httpClientFactory)
    {
        _http = httpClientFactory.CreateClient("Provider2");
    }

    public async Task<ExchangeResponse?> GetRateAsync(ExchangeRequest request)
    {
        var xmlRequest = new ExchangeXml { From = request.SourceCurrency, To = request.TargetCurrency, Amount = request.Amount };
        try
        {
            var serializer = new XmlSerializer(typeof(ExchangeXml));
            using var sw = new StringWriter();
            serializer.Serialize(sw, xmlRequest);
            var content = new StringContent(sw.ToString(), Encoding.UTF8, "application/xml");

            var response = await _http.PostAsync("/convert", content);
            response.EnsureSuccessStatusCode();

            var xml = await response.Content.ReadAsStringAsync();
            var resultSerializer = new XmlSerializer(typeof(ResultXml));
            using var sr = new StringReader(xml);
            var result = (ResultXml)resultSerializer.Deserialize(sr)!;

            return new ExchangeResponse("API2", result.Result);
        }
        catch { return null; }
    }

    [XmlRoot("Exchange")]
    public class ExchangeXml
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }

    [XmlRoot("Result")]
    public class ResultXml
    {
        [XmlText]
        public decimal Result { get; set; }
    }
}