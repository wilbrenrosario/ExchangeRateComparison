# ExchangeRateComparison

[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/download)
[![Docker](https://img.shields.io/badge/Docker-Required-blue.svg)](https://www.docker.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

## Author
* **Wilbren Rosario** - *Developer* - [GitHub](https://github.com/wilbrenrosario)

## Descripción

ExchangeRateComparison es una solución .NET 8 robusta para comparar tasas de cambio entre múltiples proveedores de manera simultánea. La aplicación implementa principios de Clean Architecture y proporciona tolerancia a fallos con manejo completo de errores.

### Características principales

- ✅ **Comparación simultánea** de múltiples proveedores de tasas de cambio
- ✅ **Tolerancia a fallos** con logica para validar un proveedor fallido
- ✅ **Clean Architecture** con separación clara de responsabilidades
- ✅ **Docker-ready** con compose para desarrollo
- ✅ **APIs mock** incluidas para testing y desarrollo

## Estructura del Proyecto

```
ExchangeRateComparison/
├── ExchangeRateComparison.Api/          # API principal ASP.NET Core
├── ExchangeRateComparison.Application/   # Lógica de aplicación y casos de uso
├── ExchangeRateComparison.Domain/       # Entidades y lógica de dominio
├── ExchangeRateComparison.Infrastructure/ # Implementaciones de acceso a datos y servicios externos
├── mocks-apis/                          # APIs mock para desarrollo y testing
│   ├── Api1Mock/                        # Mock del proveedor 1
│   ├── Api2Mock/                        # Mock del proveedor 2
│   └── Api3Mock/                        # Mock del proveedor 3
├── docker-compose.yml                   # Configuración de Docker Compose
├── ExchangeRateComparison.Tests         # Configuración de pruebas
└── README.md
```

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (Requerido para el despliegue)
- [Git](https://git-scm.com/)

## Instalación y Ejecución

### 🐳 Ejecución con Docker (Recomendado)

1. **Clonar el repositorio**
```bash
git clone https://github.com/wilbrenrosario/ExchangeRateComparison.git
cd ExchangeRateComparison
```

2. **Construir y ejecutar con Docker Compose**
```bash
docker compose build
docker compose up
```

### 🔧 Ejecución local para desarrollo

Si prefieres ejecutar localmente para desarrollo:

1. **Ejecutar las APIs mock primero**
```bash
# Terminal 1 - API Mock 1
cd mocks-apis/Api1Mock
dotnet run

# Terminal 2 - API Mock 2  
cd mocks-apis/Api2Mock
dotnet run

# Terminal 3 - API Mock 3
cd mocks-apis/Api3Mock
dotnet run

# Terminal 4 - API Principal
cd ExchangeRateComparison.Api
dotnet run
```

## Uso de la API

### Endpoints disponibles

| Endpoint | Método | Descripción |
|----------|--------|-------------|
| `/api/Exchange` | POST | Comparar tasas de cambio entre proveedores |
| `/swagger` | GET | Documentación Swagger |

### Ejemplo de uso

```bash
curl --location 'http://localhost:5000/api/Exchange' \
--header 'accept: */*' \
--header 'Content-Type: application/json' \
--data '{
  "sourceCurrency": "USD",
  "targetCurrency": "DOP",
  "amount": 100
}'
```

### Respuesta exitosa

```json
{
    "provider": "API2",
    "result": 6190.0,
    "bestRate": 61.9
}
```


## Arquitectura

### Clean Architecture

El proyecto sigue los principios de Clean Architecture:

- **Domain**: Entidades de negocio y reglas de dominio
- **Application**: Casos de uso y lógica de aplicación
- **Infrastructure**: Implementaciones de persistencia y servicios externos
- **API**: Controllers y configuración web


## Configuración

### Ports mapping

| Servicio | Puerto externo | Puerto interno |
|----------|---------------|----------------|
| Main API | 5000 | 80 |
| API1 Mock | 5051 | 81 |
| API2 Mock | 5052 | 82 |
| API3 Mock | 5053 | 83 |

## Testing

### Ejecutar pruebas unitarias
```bash
dotnet test
```


## Tecnologías utilizadas

- **Framework**: .NET 8, ASP.NET Core
- **Containerización**: Docker, Docker Compose
- **Logging**: Serilog, Microsoft.Extensions.Logging
- **Documentación**: Swagger/OpenAPI
- **Testing**: xUnit, Moq
