# PilotUtilityApi

A production-ready REST API utility for general tasks and operations, demonstrating best practices in .NET development with clean architecture, comprehensive testing, and multi-database support.

## Table of Contents

- [Description](#description)
- [Features](#features)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Building the Application](#building-the-application)
  - [Running the Application](#running-the-application)
- [Configuration](#configuration)
  - [Data Sources](#data-sources)
  - [Switching Databases](#switching-databases)
- [API Endpoints](#api-endpoints)
- [Testing](#testing)
- [Architecture](#architecture)
- [License](#license)

## Description

PilotUtilityApi is a proof-of-concept REST API built with .NET 10 that showcases modern software development practices including:

- **Clean Architecture**: Clear separation of concerns across Domain, Repository, Service, and Web layers
- **Multi-Database Support**: Seamless switching between SQL Server and PostgreSQL
- **Comprehensive Testing**: Full unit test coverage with NUnit
- **Configuration Management**: Flexible configuration with validation
- **Error Handling**: Structured exception handling with correlation IDs for troubleshooting
- **API Documentation**: OpenAPI/Swagger integration

The API currently provides testing management functionality for resetting test data across supported databases.

## Features

- **Testing Management**: Reset test data in SQL Server or PostgreSQL databases
- **Health Check**: Basic health check endpoint
- **Multi-Database**: Support for SQL Server and PostgreSQL with automatic connection management
- **Logging**: Structured logging with Serilog
- **API Versioning**: Version-neutral endpoints
- **OpenAPI Documentation**: Auto-generated API documentation with Swagger UI

## Project Structure

```
PilotUtilityApi/
├── src/
│   ├── PilotUtilityApi.Domain/           # DTOs and domain models
│   │   └── Models/
│   │       └── Responses/                 # Response models (RetrieveResponse, AboutResponse)
│   ├── PilotUtilityApi.Repositories/     # Database access layer
│   │   ├── Constants/                     # SQL scripts and constants
│   │   └── Repositories/                  # Repository implementations
│   ├── PilotUtilityApi.Services/         # Business logic layer
│   │   ├── Extensions/                    # Dependency injection extensions
│   │   └── Services/                      # Service implementations
│   ├── PilotUtilityApi.Shared/           # Shared utilities and configuration
│   │   ├── Api/                          # API extensions and middleware
│   │   ├── Configuration/                # Configuration classes
│   │   ├── Exceptions/                   # Custom exceptions
│   │   ├── Logging/                      # Logging utilities
│   │   └── Utilities/                    # General utilities
│   └── PilotUtilityApi.Web/              # Web/API layer
│       ├── Controllers/                  # API controllers
│       ├── Extensions/                   # Web extensions
│       └── appsettings.json             # Configuration file
├── test/
│   ├── PilotUtilityApi.Domain.Tests/     # Domain layer tests
│   ├── PilotUtilityApi.Repositories.Tests/ # Repository layer tests
│   ├── PilotUtilityApi.Services.Tests/   # Service layer tests
│   ├── PilotUtilityApi.Shared.Tests/     # Shared utilities tests
│   ├── PilotUtilityApi.TestingShared/    # Shared test infrastructure
│   └── PilotUtilityApi.Web.Tests/        # Web layer tests
└── docs/                                  # Documentation

```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- One of the following databases:
  - **SQL Server** (Express, Developer, or Standard edition)
  - **PostgreSQL** (version 12+)
- A code editor or IDE:
  - [Visual Studio 2026](https://visualstudio.microsoft.com/) (recommended)
  - [Visual Studio Code](https://code.visualstudio.com/)
  - [Rider](https://www.jetbrains.com/rider/)

### Building the Application

1. Clone the repository:
   ```bash
   git clone https://github.com/MikeLooper/PilotUtilityApi.git
   cd PilotUtilityApi
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

### Running the Application

1. Configure your database connection in `src/PilotUtilityApi.Web/appsettings.json` (see [Configuration](#configuration) below)

2. Run the application:
   ```bash
   cd src/PilotUtilityApi.Web
   dotnet run
   ```

3. Access the API:
   - **Swagger UI**: https://localhost:7000/swagger (or the port shown in console)
   - **Health Check**: https://localhost:7000/healthcheck

## Configuration

The application configuration is located in `src/PilotUtilityApi.Web/appsettings.json`.

### Data Sources

The `Application.DataSources` array defines database connections. Each data source has the following properties:

| Property | Type | Description | Default |
|----------|------|-------------|---------|
| `Active` | boolean | Whether this data source is currently active. **Only one can be true.** | `false` |
| `ConnectTimeout` | integer | Connection timeout in seconds | `30` |
| `DataSourceName` | string | Friendly name for the data source | - |
| `DataSource` | string | Database name | - |
| `DataSourceType` | string | Database type: `SqlServer` or `PostgreSQL` | - |
| `Host` | string | Database server hostname or IP address | `localhost` |
| `Password` | string | Database password | - |
| `Port` | integer | Database server port | `1433` (SQL Server) or `5432` (PostgreSQL) |
| `UserName` | string | Database username | - |
| `Schema` | string | Default schema | `dbo` (SQL Server) or `pilot` (PostgreSQL) |

**Example configuration:**

```json
{
  "Application": {
    "DataSources": [
      {
        "Active": true,
        "ConnectTimeout": 30,
        "DataSourceName": "NorthWind_SQL",
        "DataSource": "NorthWind",
        "DataSourceType": "SqlServer",
        "Host": "localhost",
        "Password": "your-password",
        "Port": 1433,
        "UserName": "DevUser",
        "Schema": "dbo"
      },
      {
        "Active": false,
        "ConnectTimeout": 30,
        "DataSourceName": "NorthWind_Pgs",
        "DataSource": "northwind",
        "DataSourceType": "PostgreSQL",
        "Host": "localhost",
        "Password": "your-password",
        "Port": 5432,
        "UserName": "DevUser",
        "Schema": "pilot"
      }
    ],
    "OpenApi": {
      "Title": "PilotUtilityApiDotNet",
      "Contact": {
        "Email": "MikelLooper@gmail.com",
        "Name": "Michael Looper",
        "URL": "https://github.com/MikeLooper/PilotUtilityApiDotNet"
      },
      "Description": "A proof of concept API to explore best-practices and new ideas (.NET/C#)",
      "License": "MIT",
      "Summary": "Proof of concept API",
      "Version": "0.1.1"
    }
  }
}
```

### Local Development

For local development and testing, the following User Secret can be used:
```
{
	"Application": {
		"DataSources": [
			{
				"Active": true,
				"ConnectTimeout": 30,
				"DataSourceName": "NorthWind_SQL",
				"DataSource": "NorthWind",
				"DataSourceType": "SqlServer",
				"Host": "localhost",
				"Password": "Hjm$435yVt7a",
				"Port": 1433,
				"UserName": "DevUser",
				"Schema": "dbo"
			},
			{
				"Active": false,
				"ConnectTimeout": 30,
				"DataSourceName": "NorthWind_Pgs",
				"DataSource": "northwind",
				"DataSourceType": "PostgreSQL",
				"Host": "localhost",
				"Password": "Pwo_698UVtra",
				"Port": 5432,
				"UserName": "DevUser",
				"Schema": "pilot"
			}
		]
	}
}
```

### Switching Databases

To switch between SQL Server and PostgreSQL:

1. Open `src/PilotUtilityApi.Web/appsettings.json`

2. Locate the `Application.DataSources` array

3. Set `Active: false` on the current active data source

4. Set `Active: true` on the data source you want to use

5. Restart the application

**Important**: Only one data source can have `Active: true` at a time. The application will throw a `ConfigurationException` if:
- No data sources are configured
- No data source is marked as active
- Multiple data sources are marked as active

## API Endpoints

### Testing Management

#### Reset Testing Data
Removes test records from the database tables.

- **Endpoint**: `POST /testing/reset`
- **Authentication**: None (Allow Anonymous)
- **Response Codes**:
  - `200 OK`: Successfully deleted test records. Response body contains the count of deleted rows.
  - `204 No Content`: Successfully executed, but no test records were found to delete.
  - `400 Bad Request`: An error occurred. Check the `Warning` header for details.

**Example request:**
```bash
curl -X POST https://localhost:7000/testing/reset
```

**Example success response (200):**
```json
5
```

### System

#### Health Check
Returns the health status of the API.

- **Endpoint**: `GET /healthcheck`
- **Response**: `200 OK` with body `"OK"`

## Testing

The project includes comprehensive unit tests using NUnit.

### Running All Tests

```bash
dotnet test
```

### Running Tests for a Specific Project

```bash
# Test the Shared project
dotnet test test/PilotUtiltyApi.Shared.Tests

# Test the Repositories project
dotnet test test/PilotUtiltyApi.Repositories.Tests

# Test the Services project
dotnet test test/PilotUtiltyApi.Services.Tests

# Test the Web project
dotnet test test/PilotUtiltyApi.Web.Tests
```

### Test Coverage

- **ApplicationConfiguration**: 10 tests covering validation logic
- **TestingRepository**: 5 tests covering constructor, configuration handling, and error cases
- **TestingService**: 6 tests covering business logic and repository interaction
- **TestingController**: 8 tests covering HTTP endpoints and status codes

All tests follow the Arrange-Act-Assert pattern and use mocking (Moq) for dependencies.

## Architecture

The application follows **Clean Architecture** principles with clear separation of concerns:

### Layers

1. **Domain Layer** (`PilotUtilityApi.Domain`)
   - Contains DTOs and domain models
   - No dependencies on other layers
   - Pure data structures

2. **Repository Layer** (`PilotUtilityApi.Repositories`)
   - Data access logic using Dapper
   - Database-specific implementations
   - Exception handling with correlation IDs
   - Depends on: Domain, Shared

3. **Service Layer** (`PilotUtilityApi.Services`)
   - Business logic orchestration
   - Coordinates between repositories
   - Dependency injection setup
   - Depends on: Domain, Repository, Shared

4. **Web Layer** (`PilotUtilityApi.Web`)
   - API controllers (thin layer)
   - HTTP concerns only
   - Endpoint definitions
   - Depends on: Services, Domain, Shared

5. **Shared Layer** (`PilotUtilityApi.Shared`)
   - Cross-cutting concerns
   - Configuration management
   - Logging utilities
   - Exception definitions
   - No dependencies on other application layers

### Key Patterns

- **Dependency Injection**: Constructor-based DI throughout
- **Repository Pattern**: Abstract data access behind interfaces
- **Service Pattern**: Encapsulate business logic
- **Exception Handling**: Custom exceptions with correlation IDs for tracing
- **Configuration Validation**: Startup-time configuration validation

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

**Author**: Michael Looper  
**Repository**: https://github.com/MikeLooper/PilotUtilityApi  
**Contact**: MikelLooper@gmail.com
