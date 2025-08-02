namespace ExchangeRateComparison.Domain;

public interface IExchangeRateProvider
{
    Task<ExchangeResponse?> GetRateAsync(ExchangeRequest request);
}