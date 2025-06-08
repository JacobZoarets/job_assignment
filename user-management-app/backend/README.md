# README for Backend Application

## User Management API

This is the backend component of the User Management application, built using .NET. It provides a RESTful API for managing user data, fetching users from the Random User Generator API, and serving them to the frontend application.

### Table of Contents
- [Technologies Used](#technologies-used)
- [Setup Instructions](#setup-instructions)
- [API Endpoints](#api-endpoints)
- [Database Migrations](#database-migrations)
- [Error Handling](#error-handling)
- [Testing](#testing)
- [Contributing](#contributing)

### Technologies Used
- .NET 6
- Entity Framework Core
- ASP.NET Core Web API
- Swagger for API documentation
- Dependency Injection

### Setup Instructions
1. **Clone the repository:**
   ```
   git clone <repository-url>
   cd user-management-app/backend
   ```

2. **Install dependencies:**
   Ensure you have the .NET SDK installed. You can check this by running:
   ```
   dotnet --version
   ```

3. **Configure the database:**
   Update the `appsettings.json` file with your database connection string.

4. **Run migrations:**
   To create the database schema, run:
   ```
   dotnet ef database update
   ```

5. **Start the application:**
   You can run the application using:
   ```
   dotnet run
   ```

6. **Access the API:**
   The API will be available at `http://localhost:5000/api/users`.

### API Endpoints
- **GET /api/users**: Fetch a paginated list of users.
- **GET /api/users/{id}**: Fetch detailed information about a specific user.
- **GET /api/users/search?query={searchTerm}**: Search for users based on a query string.

### Database Migrations
Migrations are managed using Entity Framework Core. You can create a new migration with:
```
dotnet ef migrations add <MigrationName>
```
And apply migrations with:
```
dotnet ef database update
```

### Error Handling
The application includes middleware for global exception handling. Any unhandled exceptions will return a user-friendly error message along with a 500 status code.

### Testing
Unit tests are included for both the controllers and services. To run the tests, navigate to the `UserManagement.Tests` directory and run:
```
dotnet test
```

### Contributing
Contributions are welcome! Please submit a pull request or open an issue for any enhancements or bug fixes.

---

This README provides a comprehensive overview of the backend application, including setup instructions, API endpoints, and testing guidelines. Make sure to replace `<repository-url>` with the actual URL of your GitHub repository.