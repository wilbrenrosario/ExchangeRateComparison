FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución y todos los proyectos
COPY ./ExchangeRateComparison.sln .
COPY ./ExchangeRateComparison.Api/ ./ExchangeRateComparison.Api/
COPY ./ExchangeRateComparison.Application/ ./ExchangeRateComparison.Application/
COPY ./ExchangeRateComparison.Domain/ ./ExchangeRateComparison.Domain/
COPY ./ExchangeRateComparison.Infrastructure/ ./ExchangeRateComparison.Infrastructure/

# Restaurar y compilar
RUN dotnet restore "ExchangeRateComparison.Api/ExchangeRateComparison.Api.csproj"
RUN dotnet publish "ExchangeRateComparison.Api/ExchangeRateComparison.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ExchangeRateComparison.Api.dll"]
