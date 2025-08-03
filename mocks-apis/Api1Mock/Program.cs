using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/rate", (HttpRequest req) =>
{
    var auth = req.Headers["Authorization"].ToString();
    if (auth != "Bearer token-api1") return Results.Unauthorized();

    // Codigo para darle dinamismo.
    var random = new Random();

    decimal min = 56m;
    decimal max = 62m;

    double randomDouble = random.NextDouble();
    decimal randomDecimal = min + (decimal)randomDouble * (max - min);

    randomDecimal = Math.Round(randomDecimal, 1);

    return Results.Json(new { rate = randomDecimal });
});

app.Urls.Add("http://*:81");

app.Run();