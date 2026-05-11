# MicrochipRegistrySystem

MicrochipRegistrySystem is a C#/.NET desktop application designed to manage animal microchip registration records for veterinary clinics and animal registration organisations. The system supports clinic management, microchip generation and assignment, animal registration, and searching linked animal and microchip information.

This project was developed as part of coursework and demonstrates layered software architecture, MVVM principles, SQL database integration, business rule validation, and Test-Driven Development (TDD).

## Features

- User login and account management
- Clinic management
  - Add clinics
  - Edit clinics
  - Delete clinics
- Generate batches of microchips
- Assign microchips to animals
- Register and manage animal details
- Search microchip and animal records
- Delete unused microchips
- Business layer validation
- Unit testing with mocks
- WPF MVVM desktop application structure

## Technologies Used

- C#
- .NET
- WPF
- MVVM Architecture
- SQL Database
- SQLite
- Visual Studio
- Unit Testing
- Test-Driven Development (TDD)

## Architecture

The project follows a layered architecture to separate responsibilities:

```text
Presentation Layer (WPF Views + ViewModels)
        ↓
Business Layer
        ↓
Data Layer / Repository Layer
        ↓
SQL Database
```

This structure improves maintainability, scalability, and testability.

## Project Structure

```text
MicrochipRegistrySystem/
├── App/                          # Main WPF application
│   ├── BusinessLayer/            # Business logic and validation
│   ├── DataLayer/                # Database access and repositories
│   ├── Models/                   # Data and entity models
│   ├── ViewModels/               # MVVM view models
│   ├── Views/                    # WPF UI views
│   ├── Services/                 # Application services/helpers
│   └── App.csproj
│
├── Tests/                        # Unit test project
│   ├── AnimalTests.cs
│   ├── ClinicTests.cs
│   ├── MicrochipTests.cs
│   ├── UserTests.cs
│   └── Tests.csproj
│
├── Database/                     # Database scripts/resources
│
├── MicrochipRegistrySystem.sln   # Visual Studio solution
├── README.md
└── .gitignore
```

## Business Rules

### Animals

- Animals contain:
  - Name
  - Species/type
  - Breed
  - Sex
  - Age
  - Optional microchip
- Each animal can only belong to one owner
- Each animal can only have one microchip assigned

### Microchips

- Each microchip has a unique identifier
- Microchips move through states:
  - Received
  - Implanted
  - Deactivated
- Only unused and valid microchips can be assigned

### Clinics

- Clinics can be created, updated, and deleted
- Clinics manage batches of microchips

## How to Run

### 1. Clone the Repository

```bash
git clone https://github.com/Skadooshky/MicrochipRegistrySystem.git
```

### 2. Open the Solution

Open:

```text
MicrochipRegistrySystem.sln
```

in Visual Studio.

### 3. Restore NuGet Packages

Restore packages if prompted.

### 4. Build the Project

```bash
dotnet build
```

### 5. Run the Application

Run the main WPF application project from Visual Studio.

## Running Tests

### Visual Studio

```text
Test > Run All Tests
```

### Terminal

```bash
dotnet test
```

## Development Focus

This project focused on:

- Applying MVVM architecture in WPF
- Using layered application design
- Implementing repository patterns
- Creating testable business logic
- Using Test-Driven Development practices
- Building SQL-backed desktop applications

## Author

Skadooshky
