# Society Management System

## Overview

The Society Management System is a layered architecture application designed to manage various aspects of a university's societies. It allows different user roles, such as students, society members, society executives, mentors, and administrators, to interact with the system based on their permissions and responsibilities.

## Architecture

Our system follows the n-tier architecture pattern, dividing the application into four distinct layers:

1. **Presentation Layer (User Interface)**
2. **Business Logic Layer (Service Layer)**
3. **Data Access Layer (Persistence Layer)**
4. **Database Layer**

### Presentation Layer (User Interface)

This layer contains the user interface components that users interact with. It includes various forms and screens:

- `Mentor_Home`
- `Admin_Home`
- `SocietyExecutive_Home`
- `SocietyMember_Home`
- `Student_Home`
- `JoinSocietyForm`
- `SocietyCreationForm`
- `EventCreationForm`
- `AnnouncementCreationForm`

### Business Logic Layer (Service Layer)

This layer contains the core business logic and application functionality:

- `SocietyService`
- `StudentService`
- `UserService`

### Data Access Layer (Persistence Layer)

This layer handles interactions with the database, performing CRUD operations:

- `SocietyDataAccess`
- `StudentDataAccess`
- `UserDataAccess`

### Database Layer

This layer consists of the database itself, where all the data is stored.

## Features

- **User Management:** Handles user authentication and authorization.
- **Society Management:** Allows creation, updating, and deletion of societies.
- **Event Management:** Allows society executives to create events, which must be approved by administrators.
- **Announcement Management:** Allows creation of announcements.
- **Membership Management:** Manages joining and leaving societies by students.

## Getting Started

### Prerequisites

- .NET Framework
- SQL Server or any other relational database management system
