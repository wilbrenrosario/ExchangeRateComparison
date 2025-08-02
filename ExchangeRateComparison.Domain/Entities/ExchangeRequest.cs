namespace ExchangeRateComparison.Domain;

public record ExchangeRequest(string SourceCurrency, string TargetCurrency, decimal Amount);