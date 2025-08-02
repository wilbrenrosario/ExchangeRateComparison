using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/exchange", (HttpRequest req) =>
{
    //var auth = req.Headers["Authorization"].ToString();
    //if (auth != "Bearer token-api3") return Results.Unauthorized();
    return Results.Json(new
    {
        statusCode = 200,
        message = "OK",
        data = new { total = 5790.0m }
    });
});

app.Urls.Add("http://*:80");
app.Run();