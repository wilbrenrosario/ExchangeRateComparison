using System.Net.Http.Json;
using ExchangeRateComparison.Domain;

namespace ExchangeRateComparison.Infrastructure.Providers;

public class Api3ExchangeProvider : IExchangeRateProvider
{
    private readonly HttpClient _http;

    public Api3ExchangeProvider(IHttpClientFactory httpClientFactory)
    {
        _http = httpClientFactory.CreateClient("Provider3");
    }

    public async Task<ExchangeResponse?> GetRateAsync(ExchangeRequest request)
    {
        var body = new
        {
            exchange = new
            {
                sourceCurrency = request.SourceCurrency,
                targetCurrency = request.TargetCurrency,
                quantity = request.Amount
            }
        };

        try
        {
            var response = await _http.PostAsJsonAsync("/exchange", body);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<Api3Response>();
            if (data?.Data?.Total is null) return null;
            return new ExchangeResponse("API3", data.Data.Total.Value);
        }
        catch { return null; }
    }

    private class Api3Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = "";
        public DataNode? Data { get; set; }
    }

    private class DataNode
    {
        public decimal? Total { get; set; }
    }
}