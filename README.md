# WanderMate Backend

WanderMate is a travel management system backend built with ASP.NET Core 8.0. It provides a robust API for managing hotels, travel packages, bookings, user authentication, and reviews.

## Features

- 🏨 **Hotel Management**
  - Create, read, update, and delete hotel listings
  - Manage hotel details including name, description, price, and images
  - Support for free cancellation and reserve now options

- 🎫 **Travel Packages**
  - Comprehensive travel package management
  - Package details including name, description, price, and images
  - Support for package reviews and bookings

- 👥 **User Management**
  - User registration and authentication
  - JWT-based authentication
  - Role-based authorization
  - Password reset functionality
  - User profile management

- 📝 **Reviews & Ratings**
  - Support for hotel and travel package reviews
  - Rating system
  - User-specific review management

- 📅 **Booking System**
  - Hotel booking management
  - Travel package booking
  - Booking history tracking
  - Integration with user profiles

- 🌍 **Destinations**
  - Destination management
  - Top destinations feature
  - Things to do at destinations

## Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **API Documentation**: Swagger/OpenAPI
- **Email Service**: SMTP Integration

## Project Structure

```
WanderMate_Backend/
├── Controllers/           # API endpoints
├── Models/               # Data models
├── DTOs/                 # Data transfer objects
├── Context/              # Database context
├── Services/             # Business logic
├── Migrations/           # Database migrations
└── Properties/           # Project properties
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 or VS Code

### Setup

1. Clone the repository
2. Update the connection string in `appsettings.json`
3. Run database migrations:
   ```
   dotnet ef database update
   ```
4. Run the application:
   ```
   dotnet run
   ```

### Configuration

The application uses `appsettings.json` for configuration including:
- Database connection string
- JWT settings
- SMTP settings
- CORS policy

## API Documentation

API documentation is available through Swagger UI at `/swagger` when running the application in development mode.

## API Endpoints

- `api/Auth` - Authentication endpoints
- `api/Hotel` - Hotel management
- `api/Booking` - Booking management
- `api/TravelPackages` - Travel package management
- `api/Review` - Review management
- `api/User` - User management
- `api/ThingsToDo` - Activities management

## Authentication

The API uses JWT bearer authentication. Include the JWT token in the Authorization header:
```
Authorization: Bearer [token]
```

## Database Schema

Key entities:
- Users
- Hotels
- TravelPackages
- Bookings
- Reviews
- Destinations
- ThingsToDo
