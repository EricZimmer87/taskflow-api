# TaskFlow.API

A RESTful Web API built with ASP.NET Core, Entity Framework Core, and SQL Server. The project was created to strengthen backend development skills and gain hands-on experience building secure APIs using JWT authentication and user-scoped data access.

## Features

- User registration and login
- JWT (JSON Web Token) authentication
- Protected API endpoints using `[Authorize]`
- Full CRUD operations for task items
- User-specific task management
- Entity Framework Core with code-first migrations
- SQL Server database integration
- DTO-based request and response models
- Dependency Injection with a custom `TokenService`

---

## Technologies Used

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- JWT Authentication
- LINQ
- Dependency Injection

---

## Authentication Flow

1. User registers or logs in
2. API generates a JWT
3. Client includes the JWT in the `Authorization` header
4. Protected endpoints validate the token
5. API extracts the authenticated user's claims and restricts access to only that user's data

Example header:

    Authorization: Bearer YOUR_JWT_TOKEN

---

## API Endpoints

### Authentication

| Method | Endpoint             | Description           |
| ------ | -------------------- | --------------------- |
| POST   | `/api/Auth/register` | Register a new user   |
| POST   | `/api/Auth/login`    | Login and receive JWT |

### Task Items

| Method | Endpoint              | Description                          |
| ------ | --------------------- | ------------------------------------ |
| GET    | `/api/TaskItems`      | Get all tasks for authenticated user |
| GET    | `/api/TaskItems/{id}` | Get a specific task                  |
| POST   | `/api/TaskItems`      | Create a task                        |
| PUT    | `/api/TaskItems/{id}` | Update a task                        |
| DELETE | `/api/TaskItems/{id}` | Delete a task                        |

---

## Example Request

### Create Task

    POST /api/TaskItems
    Authorization: Bearer YOUR_JWT_TOKEN
    Content-Type: application/json

Example JSON body:

    {
      "title": "Study ASP.NET Core",
      "description": "Practice building secure APIs",
      "isCompleted": false
    }

---

## Example Response

    {
      "taskItemId": 1,
      "title": "Study ASP.NET Core",
      "description": "Practice building secure APIs",
      "isCompleted": false,
      "createdAt": "2026-05-05T18:42:11.205Z"
    }

---

## Database

The application uses SQL Server with Entity Framework Core code-first migrations.

### Create Migration

    dotnet ef migrations add InitialCreate

### Apply Migration

    dotnet ef database update

---

## Running the Project

### Clone Repository

    git clone https://github.com/EricZimmer87/taskflow-api.git

### Navigate to Project

    cd TaskFlow

### Run the API

    dotnet run

---

## JWT Configuration

JWT settings are configured in `appsettings.json`.

Example:

    "Jwt": {
      "Key": "FAKE_KEY_CHANGE_FOR_PRODUCTION_123456789",
      "Issuer": "TaskFlow.API",
      "Audience": "TaskFlow.Client",
      "ExpiresInMinutes": 60
    }

The included key is a development-only placeholder and should not be used in production.

---

## What I Learned

This project helped reinforce concepts including:

- RESTful API design
- JWT authentication and authorization
- Claims-based identity
- Entity Framework Core code-first workflow
- DTO usage and separation of concerns
- Dependency Injection
- Secure user-specific data access
- ASP.NET Core middleware and request pipeline

---

## Future Improvements

- Request validation attributes
- Pagination and filtering
- Refresh tokens
- Unit testing
- Docker support
- Frontend client integration

---

## Author

Eric Zimmer

GitHub:  
https://github.com/EricZimmer87

Portfolio Website:  
https://www.ejzimmer.com
