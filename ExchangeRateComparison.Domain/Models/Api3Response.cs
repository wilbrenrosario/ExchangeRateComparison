namespace ExchangeRateComparison.Domain.Models;
public class Api3Response
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = "";
    public DataNode? Data { get; set; }
}