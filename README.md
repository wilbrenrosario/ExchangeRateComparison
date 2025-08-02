# ExchangeRateComparison

## Autor:  Wilbren Rosario

ExchangeRateComparison es una solución .NET 8 para comparar tasas de cambio entre diferentes fuentes. Incluye una API principal y proyectos de dominio, aplicación e infraestructura, así como mocks para APIs externas.

## Estructura del Proyecto

- **ExchangeRateComparison.Api**: API principal ASP.NET Core.
- **ExchangeRateComparison.Application**: Lógica de aplicación y casos de uso.
- **ExchangeRateComparison.Domain**: Entidades y lógica de dominio.
- **ExchangeRateComparison.Infrastructure**: Implementaciones de acceso a datos y servicios externos.
- **mocks-apis**: Mocks de APIs externas para pruebas y desarrollo.

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Docker (Es requerido tenerlo para el despliegue)

## Ejecución local

1. Restaura los paquetes NuGet:
   ```sh
   dotnet restore
   ```

2. Ejecuta la API:
   ```sh
   dotnet run --project ExchangeRateComparison.Api/ExchangeRateComparison.Api.csproj
   ```

3. Accede a la documentación Swagger en `http://localhost/swagger` (puerto configurable).

## Uso con Docker (RECOMENDADO)

```sh
docker compose build
docker compose up
```

```CURL

curl --location 'http://localhost:5000/api/Exchange' \
--header 'accept: */*' \
--header 'Content-Type: application/json' \
--data '{
  "sourceCurrency": "USD",
  "targetCurrency": "DOP",
  "amount": 100
}'

```

## Apis Externas

Incluye mocks en la carpeta `mocks-apis` para simular APIs externas.


---

> Proyecto generado para comparar tasas de cambio de manera.