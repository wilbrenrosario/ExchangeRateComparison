using Microsoft.AspNetCore.Mvc;
using ExchangeRateComparison.Application;
using ExchangeRateComparison.Domain;

namespace ExchangeRateComparison.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExchangeController : ControllerBase
{
    private readonly ExchangeRateService _service;

    public ExchangeController(ExchangeRateService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> GetBestExchangeRate([FromBody] ExchangeRequest request)
    {
        var bestOffer = await _service.GetBestOfferAsync(request);
        if (bestOffer is null)
            return NotFound(new { message = "No valid exchange rate found." });

        return Ok(bestOffer);
    }
}