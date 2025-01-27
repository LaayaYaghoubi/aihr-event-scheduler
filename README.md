# AIHR Event Scheduler

## Overview

This project is an event scheduling application built with a clean architecture and service layer for the backend, and Angular with Angular Material for the frontend. The application supports real-time communication using SignalR and includes features such as event management, authentication, and notifications.

### How to Run
To run the application, navigate to the root directory and use the following command:
```bash
docker compose up -d 
```
The application will be available at http://localhost:81

## Backend

### Technologies Used
- **.NET 8.0**: For building the backend services.
- **SignalR**: For real-time communication.
- **Entity Framework Core**: For database interactions.
- **ASP.NET Core Identity**: For authentication and user management.

### Architecture
The backend follows a clean architecture with a service layer. This ensures a separation of concerns and makes the codebase more maintainable and testable.

### Features
- **Event Management**: Add, edit, delete, and view events with pagination and sorting by start date.
- **Real-Time Notifications**: Users are notified of scheduled events within 24 hours using SignalR.
- **Authentication**: User authentication is handled using ASP.NET Core Identity.
- **Database Initialization**: The database is automatically created on application start for ease of development and assessment. Note that this is not a recommended practice for production environments.

## Frontend
### Technologies Used
- **Angular 19.1.0**: For building the frontend application.
- **AngularMaterial**: For UI components and styling.

### Features
- **Dashboard**: A welcoming dashboard with navigation to the events list.
- **Event Management**: Add, edit, delete, and view events with pagination and sorting by start date.
- **Real-Time Notifications**: Users are notified of scheduled events within 24 hours using a dialog.
- **Authentication**: User authentication is integrated with the backend.
- **Responsive Design**: Some parts of the application may not be fully responsive, and validation and error handling could be improved.


# Notes
#### Development Database: 
The database initializes automatically upon application startup. This is for ease of assessment and not recommended for production.
#### Real-Time Notifications: 
Notifications trigger when events are within 24 hours. Once acknowledged, they won't be resent.
#### Frontend Work in Progress: 
While functional, some areas require further work on responsiveness, validation, and error handling.

### Optional Features (Implemented)
- Authentication: Only authenticated users can manage events. Each user receives personalized notifications specific to their scheduled events.
- Error Handling: Basic validations for input fields.
- Unit Tests: Added tests for core backend.

## Conclusion
This project showcases an event scheduling system built with modern tools and best practices. Implementing the frontend application from scratch was both challenging and rewarding, providing a deeper understanding of UI development and integration. The process was a valuable learning experience, particularly in real-time communication, clean architecture, and creating a seamless user experience. I welcome feedback and suggestions for improvement to further refine this application.