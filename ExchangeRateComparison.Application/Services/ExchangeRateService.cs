using ExchangeRateComparison.Domain;

namespace ExchangeRateComparison.Application;

public class ExchangeRateService
{
    private readonly IEnumerable<IExchangeRateProvider> _providers;

    public ExchangeRateService(IEnumerable<IExchangeRateProvider> providers)
    {
        _providers = providers;
    }

    public async Task<ExchangeResponse?> GetBestOfferAsync(ExchangeRequest request)
    {
        var tasks = _providers.Select(p => p.GetRateAsync(request));
        var results = await Task.WhenAll(tasks);

        return results
            .Where(r => r is not null)
            .OrderByDescending(r => r!.Result)
            .FirstOrDefault();
    }
}