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

            builder.Services.AddHttpClient("Provider1", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5051");
                client.DefaultRequestHeaders.Add("Accept", "*/*");
            });
            builder.Services.AddHttpClient("Provider2", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5051");
                client.DefaultRequestHeaders.Add("Accept", "*/*");
            });
            builder.Services.AddHttpClient("Provider3", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5051");
                client.DefaultRequestHeaders.Add("Accept", "*/*");
            });

            builder.Services.AddScoped<IExchangeRateProvider, Api1ExchangeProvider>();
            builder.Services.AddScoped<IExchangeRateProvider, Api2ExchangeProvider>();
            builder.Services.AddScoped<IExchangeRateProvider, Api3ExchangeProvider>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
