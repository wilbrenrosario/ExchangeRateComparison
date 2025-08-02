namespace ApiMock3.models;
public class ExchangePayload
{
    public string sourceCurrency { get; set; } = string.Empty;
    public string targetCurrency { get; set; } = string.Empty;
    public decimal quantity { get; set; }
}