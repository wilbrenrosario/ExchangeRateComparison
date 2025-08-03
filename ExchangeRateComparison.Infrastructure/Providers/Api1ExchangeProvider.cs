using System.Text.Json;
using ExchangeRateComparison.Domain;
using ExchangeRateComparison.Domain.Models;

namespace ExchangeRateComparison.Infrastructure.Providers;

public class Api1ExchangeProvider : IExchangeRateProvider
{
    private readonly HttpClient _http;
    private readonly ILogger<Api1ExchangeProvider> _logger;

    public Api1ExchangeProvider(IHttpClientFactory httpClientFactory, ILogger<Api1ExchangeProvider> logger)
    {
        _http = httpClientFactory.CreateClient("Provider1");
        _logger = logger;
    }

    public async Task<ExchangeResponse?> GetRateAsync(ExchangeRequest request)
    {
        var body = new { from = request.SourceCurrency, to = request.TargetCurrency, value = request.Amount };
        try
        {
            _logger.LogInformation("Calling API1 with body: {@Body}", body);

            var response = await _http.PostAsJsonAsync("/rate", body);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<Api1Response>();
            if (data?.Rate is null) return null;

            _logger.LogInformation("## Response API1 with: {json}", JsonSerializer.Serialize(data));
            return new ExchangeResponse("API1", request.Amount* data.Rate.Value, Math.Round(data.Rate.Value, 2));
        }
        catch (Exception e){
            _logger.LogInformation("Error API1 con mensaje: {@msg}", e.Message);
            return null; }
    }
}