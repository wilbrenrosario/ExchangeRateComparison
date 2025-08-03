namespace ExchangeRateComparison.Domain;

public record ExchangeResponse(string Provider, decimal Result, decimal bestRate);