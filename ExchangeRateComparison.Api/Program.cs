using ExchangeRateComparison.Application;
using ExchangeRateComparison.Domain;
using ExchangeRateComparison.Infrastructure.Providers;

namespace ExchangeRateComparison.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ExchangeRateService>();

            // Detectar si está ejecutándose en un contenedor
            bool isRunningInContainer = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

            // Configurar las URLs base según el entorno
            string api1BaseUrl = isRunningInContainer ? "http://api1:81" : "http://localhost:81";
            string api2BaseUrl = isRunningInContainer ? "http://api2:82" : "http://localhost:82";
            string api3BaseUrl = isRunningInContainer ? "http://api3:83" : "http://localhost:83";

            builder.Services.AddHttpClient("Provider1", client =>
            {
                client.BaseAddress = new Uri(api1BaseUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer token-api1");
            });

            builder.Services.AddHttpClient("Provider2", client =>
            {
                client.BaseAddress = new Uri(api2BaseUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer token-api2");
            });

            builder.Services.AddHttpClient("Provider3", client =>
            {
                client.BaseAddress = new Uri(api3BaseUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer token-api3");
            });

            builder.Services.AddScoped<IExchangeRateProvider, Api1ExchangeProvider>();
            builder.Services.AddScoped<IExchangeRateProvider, Api2ExchangeProvider>();
            builder.Services.AddScoped<IExchangeRateProvider, Api3ExchangeProvider>();

            // Log
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            if (!app.Environment.IsProduction())
                app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
