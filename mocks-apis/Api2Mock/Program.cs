using ApiMock2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/convert", async (HttpContext context) =>
{
    if (context.Request.Headers["Authorization"] != "Bearer token-api2")
    {
        context.Response.StatusCode = 401;
        return;
    }

    string xmlBody;
    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
    {
        xmlBody = await reader.ReadToEndAsync();
    }

    var serializer = new XmlSerializer(typeof(ExchangeXml));
    ExchangeXml request;
    using (var stringReader = new StringReader(xmlBody))
    {
        request = (ExchangeXml)serializer.Deserialize(stringReader);
    }

    // Codigo para darle dinamismo.
    var random = new Random();
    decimal min = 56m;
    decimal max = 62m;
    double randomDouble = random.NextDouble();
    decimal randomDecimal = min + (decimal)randomDouble * (max - min);
    randomDecimal = Math.Round(randomDecimal, 1);

    decimal total = Math.Round(request.Amount * randomDecimal, 2);

    context.Response.ContentType = "application/xml";

    // Respuesta manual sin serialización automática
    await context.Response.WriteAsync($@"<ExchangeResult>
            <Rate>{randomDecimal}</Rate>
            <Total>{total}</Total>
        </ExchangeResult>");
        });

app.Urls.Add("http://*:82");
app.Run();