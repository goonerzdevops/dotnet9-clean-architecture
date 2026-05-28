# Dotnet9CleanArchitecture

.NET 9 Clean Architecture with CQRS, PostgreSQL, and MediatR — built for a 4-member team.

## Architecture

```
┌─────────────────────────────────────────────────┐
│                    API Layer                      │
│  Controllers, Middleware, DI, Swagger, Serilog    │
├─────────────────────────────────────────────────┤
│               Application Layer                   │
│  CQRS (Commands/Queries), DTOs, Validators,       │
│  Behaviors (Pipeline), Interfaces                 │
├─────────────────────────────────────────────────┤
│               Infrastructure Layer                │
│  EF Core, PostgreSQL, Repositories, Configs       │
├─────────────────────────────────────────────────┤
│                 Domain Layer                      │
│  Entities, Value Objects, Domain Events,          │
│  Interfaces, Base Classes                         │
└─────────────────────────────────────────────────┘
```

## Tech Stack

| Component         | Technology                          |
|-------------------|-------------------------------------|
| Framework         | .NET 9 LTS                          |
| Database          | PostgreSQL 16                       |
| ORM               | EF Core 9                           |
| CQRS              | MediatR 12                          |
| Validation        | FluentValidation                    |
| Mapping           | Mapster                             |
| Logging           | Serilog                             |
| API Docs          | Swagger / OpenAPI                   |
| Testing           | xUnit, Moq, FluentAssertions        |
| Containerization  | Docker, Docker Compose              |
| CI/CD             | GitHub Actions                      |

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/) (for PostgreSQL)
- [EF Core CLI tools](https://learn.microsoft.com/en-us/ef/core/cli/)

```bash
dotnet tool install --global dotnet-ef
```

### Setup

```bash
# 1. Clone
git clone https://github.com/goonerzdevops/dotnet9-clean-architecture.git
cd dotnet9-clean-architecture

# 2. Start PostgreSQL
docker compose up -d postgres

# 3. Apply migrations
cd src/Dotnet9CleanArchitecture.Api
dotnet ef database update --project ../Dotnet9CleanArchitecture.Infrastructure

# 4. Run API
dotnet run
```

API available at: `http://localhost:5000/swagger`

### Docker (Full)

```bash
docker compose up -d
```

## Project Structure

```
├── src/
│   ├── Dotnet9CleanArchitecture.Domain/        # Entities, Events, Interfaces
│   ├── Dotnet9CleanArchitecture.Application/   # CQRS, DTOs, Validators
│   ├── Dotnet9CleanArchitecture.Infrastructure/ # EF Core, Repos, Migrations
│   └── Dotnet9CleanArchitecture.Api/           # Controllers, Middleware, DI
├── tests/
│   ├── Dotnet9CleanArchitecture.Domain.Tests/
│   └── Dotnet9CleanArchitecture.Application.Tests/
├── docker-compose.yml
├── Directory.Build.props
└── .editorconfig
```

## Team Workflow (4 Members)

### Branch Strategy

```
main          ← Production-ready code
├── develop   ← Integration branch
│   ├── feature/product-crud    ← Member A
│   ├── feature/auth            ← Member B
│   ├── feature/order-module    ← Member C
│   └── feature/reporting       ← Member D
```

### Naming Convention

- Feature branches: `feature/<module-name>`
- Bug fixes: `fix/<issue-description>`
- Hotfixes: `hotfix/<critical-fix>`

### PR Rules

- Minimum 1 approval required
- All CI checks must pass
- Squash merge to `develop`
- Merge commit to `main` (release)

## API Endpoints

| Method | Endpoint              | Description          |
|--------|-----------------------|----------------------|
| GET    | `/api/v1/products`    | Get all products     |
| GET    | `/api/v1/products/{id}` | Get product by ID  |
| POST   | `/api/v1/products`    | Create product       |
| PUT    | `/api/v1/products/{id}` | Update product     |
| DELETE | `/api/v1/products/{id}` | Delete product     |
| GET    | `/health`             | Health check         |

## License

MIT
