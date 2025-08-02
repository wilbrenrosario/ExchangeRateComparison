using System.Net.Http;
using System.Net.Http.Json;
using ExchangeRateComparison.Domain;

namespace ExchangeRateComparison.Infrastructure.Providers;

public class Api1ExchangeProvider : IExchangeRateProvider
{
    private readonly HttpClient _http;

    public Api1ExchangeProvider(IHttpClientFactory httpClientFactory)
    {
        _http = httpClientFactory.CreateClient("Provider1");
    }

    public async Task<ExchangeResponse?> GetRateAsync(ExchangeRequest request)
    {
        var body = new { from = request.SourceCurrency, to = request.TargetCurrency, value = request.Amount };
        try
        {
            var response = await _http.PostAsJsonAsync("/rate", body);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<Api1Response>();
            if (data?.Rate is null) return null;
            return new ExchangeResponse("API1", request.Amount * data.Rate.Value);
        }
        catch { return null; }
    }

    private class Api1Response { public decimal? Rate { get; set; } }
}