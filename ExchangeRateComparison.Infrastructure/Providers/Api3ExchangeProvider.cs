using System.Text.Json;
using ExchangeRateComparison.Domain;
using ExchangeRateComparison.Domain.Models;

namespace ExchangeRateComparison.Infrastructure.Providers;

public class Api3ExchangeProvider : IExchangeRateProvider
{
    private readonly HttpClient _http;
    private readonly ILogger<Api3ExchangeProvider> _logger;

    public Api3ExchangeProvider(IHttpClientFactory httpClientFactory, ILogger<Api3ExchangeProvider> logger)
    {
        _http = httpClientFactory.CreateClient("Provider3");
        _logger = logger;
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
            _logger.LogInformation("Calling API3 with body: {@Body}", body);

            var response = await _http.PostAsJsonAsync("/exchange", body);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<Api3Response>();
            if (data?.Data?.Total is null) return null;

            _logger.LogInformation("## Response API3 with: {json}", JsonSerializer.Serialize(data));
            return new ExchangeResponse("API3", data.Data.Total.Value);
        }
        catch (Exception e) {
            _logger.LogInformation("Error API3 con mensaje: {@msg}", e.Message); 
            return null; }
    }  
}