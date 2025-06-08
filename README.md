# README.md for User Management App

# User Management Application

This is a full-stack User Management application built with a React frontend and a .NET backend. The application allows users to view a list of users fetched from the Random User Generator API, view detailed information about each user, and navigate between different screens.

## Table of Contents

- [Technologies Used](#technologies-used)
- [Features](#features)
- [Setup Instructions](#setup-instructions)
- [API Documentation](#api-documentation)
- [Assumptions](#assumptions)

## Technologies Used

- **Frontend**: React, Next.js, TypeScript, Tailwind CSS
- **Backend**: .NET 6, Entity Framework Core
- **Database**: SQL Server (or any other database of your choice)
- **API**: Random User Generator API (https://randomuser.me/)

## Features

- User listing with pagination
- Search functionality to filter users
- Detailed user view with personal information
- Responsive design for mobile and desktop
- Error handling and loading states

## Setup Instructions

### Frontend

1. Navigate to the `frontend` directory:
   ```
   cd frontend
   ```

2. Install dependencies:
   ```
   npm install
   ```

3. Start the development server:
   ```
   npm run dev
   ```

4. Open your browser and go to `http://localhost:3000` to view the application.

### Backend

1. Navigate to the `backend` directory:
   ```
   cd backend
   ```

2. Restore the NuGet packages:
   ```
   dotnet restore
   ```

3. Update the database (if using Entity Framework):
   ```
   dotnet ef database update
   ```

4. Start the backend server:
   ```
   dotnet run
   ```

5. The backend API will be available at `http://localhost:5000`.

## API Documentation

The backend API provides the following endpoints:

- `GET /api/users`: Fetch a paginated list of users.
- `GET /api/users/{id}`: Fetch detailed information about a specific user.
- `GET /api/users/search?query={searchTerm}`: Search for users based on a query string.

Refer to the Swagger documentation for more details on the API endpoints.

## Assumptions

- The application assumes that the Random User Generator API is available and accessible.
- The database connection string and other configurations are set up correctly in the `appsettings.json` file in the backend.
- The application is designed to be responsive and should work on both mobile and desktop devices.

Feel free to explore the code and make modifications as needed!