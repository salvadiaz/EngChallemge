# Eng's Challenge

## Overview

Your task is to create a service capable of managing user data. This service will form a critical part of our system, enabling us to handle user information efficiently.

## User Model

Your service should manage user data structured as follows:

- **Users Table**
    - **Id**: Unique identifier for the user
    - **Name**: User's full name
    - **Birthdate**: User's date of birth
    - **Active**: Boolean indicating whether the user is active

## Functional Requirements

Your implementation should provide the following endpoints:

- **Create a New User**: The endpoint must allow the creation of a new user with the "Active" status set to true by default.
- **Update User State**: This endpoint should enable updating the "Active" status of an existing user.
- **Delete Users**: Provide functionality to delete users from the database.
- **List All Active Users**: This endpoint should return a list of all users with the "Active" status set to true.

## Running the Application

To run the application, you need Docker Compose. Follow these steps:

1. Ensure Docker and Docker Compose are installed on your machine.
2. Navigate to the project directory.
3. Run the following command to start the application:
   ```sh
   docker-compose up -d
   ```

## Swagger URL

Once the application is running, you can access the Swagger documentation at:
```
http://localhost:5000/swagger/index.html
```

## Endpoints

The following endpoints do not require authorization:

- **Register a New User**: `POST /api/users`
- **Log In**: `POST /api/users/login`

All other endpoints require authorization, and you must be logged in.
