using System.Text;
using System.Xml.Serialization;
using ExchangeRateComparison.Domain;
using ExchangeRateComparison.Domain.Models;

namespace ExchangeRateComparison.Infrastructure.Providers;

public class Api2ExchangeProvider : IExchangeRateProvider
{
    private readonly HttpClient _http;
    private readonly ILogger<Api2ExchangeProvider> _logger;

    public Api2ExchangeProvider(IHttpClientFactory httpClientFactory, ILogger<Api2ExchangeProvider> logger)
    {
        _http = httpClientFactory.CreateClient("Provider2");
        _logger = logger;

    }

    public async Task<ExchangeResponse?> GetRateAsync(ExchangeRequest request)
    {
        var xmlRequest = new ExchangeXml { From = request.SourceCurrency, To = request.TargetCurrency, Amount = request.Amount };
        try
        {
            _logger.LogInformation("Calling API2 with bodyXML: {@Body}", xmlRequest.ToString());

            var serializer = new XmlSerializer(typeof(ExchangeXml));
            using var sw = new StringWriter();
            serializer.Serialize(sw, xmlRequest);
            var content = new StringContent(sw.ToString(), Encoding.UTF8, "application/xml");

            var response = await _http.PostAsync("/convert", content);
            response.EnsureSuccessStatusCode();

            var xml = await response.Content.ReadAsStringAsync();
            var resultSerializer = new XmlSerializer(typeof(ExchangeResult));
            using var sr = new StringReader(xml);
            var result = (ExchangeResult)resultSerializer.Deserialize(sr)!;

            _logger.LogInformation("## Response API2 with: {json}", xml);
            return new ExchangeResponse("API2", result.Total, result.Rate);
        }
        catch (Exception e) {
            _logger.LogInformation("Error API2 con mensaje: {@msg}", e.Message);
            return null; }
    }  
}