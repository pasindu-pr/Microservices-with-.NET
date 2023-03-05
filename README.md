
# Microservices .NET
![enter image description here](https://res.cloudinary.com/dxrksxul/image/upload/v1678027064/Github/Microservices_2_pwzogv.jpg)

This project is a sample microservice application using .NET, RabbitMQ, and MongoDB. The purpose of this project is to demonstrate how to build a simple microservice architecture using these technologies.

## Architecture

The architecture of the project is composed of two microservices:

-   **Catalog Service**: This service is responsible for managing catalog items. It exposes REST APIs to create, update, and retrieve orders.
    
-   **Inventory Service**: This service is responsible for managing inventory. It consumes messages from a RabbitMQ queue and processes payments using a third-party payment gateway.
