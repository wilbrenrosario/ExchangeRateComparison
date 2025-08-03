using ApiMock3.models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/exchange", async (HttpRequest req) =>
{
    var auth = req.Headers["Authorization"].ToString();
    if (auth != "Bearer token-api3") return Results.Unauthorized();

    var json = await req.ReadFromJsonAsync<ExchangeRequestJson>();

    if (json?.exchange is null)
        return Results.BadRequest(new { message = "Missing 'exchange' payload" });

    var random = new Random();

    decimal min = 56m;
    decimal max = 62m;

    double randomDouble = random.NextDouble();
    decimal randomDecimal = min + (decimal)randomDouble * (max - min);
    randomDecimal = Math.Round(randomDecimal, 1);

    decimal total = Math.Round(json.exchange.quantity * randomDecimal, 2);

    return Results.Json(new
    {
        statusCode = 200,
        message = "OK",
        data = new { total }
    });
});

app.Urls.Add("http://*:83");

app.Run();