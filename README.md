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

## Authors
- Sara & Yehudit
