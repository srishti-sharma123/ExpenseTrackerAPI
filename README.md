# 💰 Expense Tracker API

A RESTful Web API built with **ASP.NET Core** to manage personal expenses and transactions. This project demonstrates backend development, API design, database integration, and JWT-based authentication.

## 🚀 Features

- 🔐 User registration and login
- 🔑 JWT authentication and authorization
- 💸 Create expense transactions
- 📋 Retrieve transaction details
- ✏️ Update existing transactions
- 🗑️ Delete transactions
- 🛡️ Protected endpoints using `[Authorize]`
- 📖 API testing and documentation with Swagger

## 🛠️ Tech Stack

- **Language:** C#
- **Framework:** ASP.NET Core Web API
- **Database:** SQL Server (if configured in your project)
- **Authentication:** JWT Bearer Authentication
- **API Documentation:** Swagger / OpenAPI
- **Tools:** Visual Studio, Git, GitHub

## 📂 Project Overview

The application follows a structured backend architecture with API controllers and a repository layer for handling transaction-related operations.

Core functionality includes:

- Handling HTTP requests through API endpoints
- Managing transaction data through repository methods
- Authenticating users with JWT tokens
- Protecting endpoints against unauthorized access
- Testing API functionality through Swagger UI

## ⚙️ Getting Started

### Prerequisites

- Visual Studio
- .NET SDK compatible with the project
- SQL Server, if required by the configured database

### Installation

**1. Clone the repository**

```bash
git clone https://github.com/srishti-sharma123/ExpenseTrackerAPI.git
```

**2. Open the project**

Open the solution file (`.sln`) in Visual Studio.

**3. Configure the application**

Configure your database connection string and JWT settings in your local configuration according to the project's existing setup.

Do not commit passwords, secret keys, or other credentials to GitHub.

**4. Restore dependencies**

```bash
dotnet restore
```

**5. Run the application**

Navigate to the directory containing the API project file (`.csproj`) and run:

```bash
dotnet run
```

**6. Test the API**

Open the Swagger URL displayed when the application starts, if Swagger is enabled in the current environment.

## 🔒 Authentication Flow

1. Register a user.
2. Log in using the registered credentials.
3. Obtain the JWT access token returned by the API.
4. Authorize Swagger using the token.
5. Send requests to protected endpoints.

## 🧪 Testing

The API endpoints can be tested using Swagger UI.

Test the authentication flow and verify that protected endpoints require a valid token.

## 🎯 Learning Outcomes

- Building RESTful APIs with ASP.NET Core
- Implementing CRUD operations
- Applying repository-pattern concepts
- Implementing JWT authentication and authorization
- Testing APIs with Swagger
- Organizing and maintaining a backend project

## 🔮 Future Improvements

- Develop an ASP.NET Core MVC frontend
- Add expense categories and filtering
- Implement pagination and improved validation
- Add automated unit and integration tests
- Deploy the application to the cloud

## 👩‍💻 Author

**Srishti Sharma**

- GitHub: [srishti-sharma123](https://github.com/srishti-sharma123)

---

*Built as a hands-on project to strengthen backend development skills with .NET.*
