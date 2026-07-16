# Product Management API

A RESTful Product Management API built using ASP.NET Core 8 following Clean Architecture principles. The project supports CRUD operations, JWT Authentication, API Versioning, AutoMapper, FluentValidation, Serilog logging, and SQL Server.

## Features

- CRUD Operations
- JWT Authentication & Authorization
- Role-Based Authorization
- API Versioning
- Entity Framework Core
- SQL Server
- Repository Pattern
- Clean Architecture
- AutoMapper
- FluentValidation
- Serilog Logging
- Swagger UI
- Docker Support
- Unit Testing (xUnit & Moq)

## Tech Stack

- ASP.NET Core 8
- C#
- SQL Server
- Entity Framework Core
- JWT
- AutoMapper
- FluentValidation
- Serilog
- Swagger
- Docker
- xUnit
- Moq

## Project Structure

```
ProductApi
│
├── API
├── Application
├── Domain
├── Infrastructure
├── Application.Tests
├── docker-compose.yml
└── ProductApi.sln
```

## Getting Started

### Clone Repository

```bash
git clone https://github.com/ShwetaMedhe/ProductApi.git
```

### Open Project

Open **ProductApi.sln** using Visual Studio 2022

## API Endpoints

| Method | Endpoint | Description |
|---------|----------|-------------|
| GET | /api/v1/Products | Get All Products |
| GET | /api/v1/Products/{id} | Get Product By Id |
| POST | /api/v1/Products | Create Product |
| PUT | /api/v1/Products/{id} | Update Product |
| DELETE | /api/v1/Products/{id} | Delete Product |

## Authentication

JWT Authentication is used.

Roles:

- Admin
- User

## Output Screenshots

Project output screenshots are available in the **Screenshots** folder.

## Author

Shweta Medhe



GitHub:
https://github.com/ShwetaMedhe
