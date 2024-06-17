# All Products Store Project

## Overview
Our project is a store selling various types of products. It is constructed using ASP.NET Core 8 based on REST architecture.

## Technologies Used
- **Backend Framework**: ASP.NET Core 8
- **ORM**: Entity Framework (EF), DB-first approach
- **Object Mapping**: AutoMapper
- **Configuration Management**: Config files for managing connection strings

## Security Features
- **Client and Server Validation**: Ensures robust security
- **Password Strength Check**: Enhanced security using ZXCVBN library

## Architecture
The application is built according to a layered architecture, divided into:
- **Controllers**: Handle HTTP requests and responses
- **Services**: Contain business logic
- **Repositories**: Interact with the database

### Communication Between Layers
- **Dependency Injection**: Ensures encapsulation and flexibility
- **DTO (Data Transfer Objects)**: Prevent circular dependencies and maintain encapsulation between layers

## Asynchronous Programming
- **Async/Await**: Utilized throughout the project for scalability and responsive performance

## Error Handling
- **Middleware**: Catches and handles errors
- **Logging**: Logs events such as problems, errors, or informational messages
- **Email Notification**: Used for error reporting

## API Documentation
- **Swagger**: Provides comprehensive API documentation

## Client-Side Development
- **Technologies**: HTML5, CSS, JavaScript

## Database
- **Database Server**: SQL Server

## Coding Standards
- **Clean Code Principles**: Followed meticulously throughout the codebase

## Testing

In line with the project's emphasis on reliability and quality, an extensive suite of tests has been implemented to validate the User-related operations. The testing files encompass various scenarios to ensure the correctness of the UserRepository class methods:


### UserUnitTests.cs

  
- **TestRegister_NewUser_Success**: Validates successful registration of a new user.

  
- **TestRegister_NewUser_InSuccess**: Tests behavior when attempting to register a user with existing credentials.

  
- **TestLogin_Successful**: Ensures successful login with the correct username and password.

  
- **TestLogin_Failed**: Tests login failure with an incorrect password.

  
- **Register_ExceptionThrown_ExceptionIsThrown**: Verifies that an exception is thrown during user registration.

  
- **Update_ExistingUser_SuccessfullyUpdated**: Validates the successful update of an existing user's information.

  
- **Update_NonExistingUser_ReturnsNull**: Confirms that updating a non-existing user returns null.


### UserIntegrationTest.cs

  
- **GetUser_ValidCredentials_ReturnsUser**: Tests the retrieval of a user with valid credentials from the database.

## Authors
- Sara & Yehudit
