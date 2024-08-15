## Overview
This is a simple event-driven microservices application built with C# and .NET 6+. The application consists of three microservices: `UserService`, `OrderService`, and `NotificationService`. The services communicate using RabbitMQ, and data is stored in a PostgreSQL database.

## Microservices
- **UserService**: Manages user data with CRUD operations.
- **OrderService**: Manages orders and publishes an event when a new order is created.
- **NotificationService**: Listens for order creation events and sends a mock notification.

## Technology Stack
- .NET 6+
- ASP.NET Core
- RabbitMQ
- PostgreSQL
- Docker

## Setup Instructions
1. Ensure Docker and Docker Compose are installed on your machine.
2. Clone the repository.
3. Navigate to the solution directory.
4. Run `docker-compose up --build` to start the services.
5. Access the services via:
   - UserService: `http://localhost:7085`
   - OrderService: `http://localhost:7176`
   - NotificationService: `http://localhost:7175`

## Architectural Decisions
- **Event-Driven Architecture**: This allows for decoupled services that can scale independently.
- **RabbitMQ**: Chosen for its reliability and widespread support for messaging between microservices.
- **PostgreSQL**: A robust, open-source relational database that integrates well with .NET and Docker.
- **Docker**: Enables easy containerization and orchestration of microservices.