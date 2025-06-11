<div align="center">

# Soccer Results Portal

A comprehensive web-based soccer management system built with ASP.NET Core MVC.

![Last Commit](https://img.shields.io/github/last-commit/emirbesir/SoccerPortal?style=flat&logo=git&logoColor=white&color=0080ff)
![Top Language](https://img.shields.io/github/languages/top/emirbesir/SoccerPortal?style=flat&color=0080ff)

![HTML5](https://img.shields.io/badge/HTML5-E34F26.svg?style=flat&logo=html5&logoColor=white)
![CSS](https://img.shields.io/badge/CSS-663399.svg?style=flat&logo=css&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3.svg?style=flat&logo=bootstrap&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4.svg?style=flat&logo=dotnet&logoColor=white)

</div>



## Project Overview

The Soccer Results Portal is a full-featured web application designed to manage soccer teams, players, matches, fixtures, and venues. It provides an intuitive interface for viewing match results and upcoming fixtures, with administrative capabilities for data management.

## Features

- **Team Management**: Create, view, edit, and delete soccer teams
- **Player Management**: Manage player profiles with team associations
- **Match Results**: Record and display match results with scores
- **Fixtures**: Schedule and manage upcoming matches
- **Venue Management**: Maintain venue information for matches
- **User Authentication**: Secure login system with role-based access
- **Responsive Design**: Bootstrap-powered UI that works on all devices
- **Administrative Panel**: Role-based administrative controls

## Technology Stack

- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap 5, Razor Pages
- **Backend**: ASP.NET Core 8.0, C#
- **Database**: Microsoft SQL Server with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Architecture**: MVC (Model-View-Controller) Pattern

## Prerequisites

- .NET 8.0 SDK
- Microsoft SQL Server (LocalDB or full installation)

## Installation and Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/emirbesir/SoccerPortal.git
   cd SoccerPortal
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Update database connection string**
   - Open `appsettings.json`
   - Modify the `DefaultConnection` string if needed

4. **Run database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Build and run the application**
   ```bash
   dotnet build
   dotnet run
   ```

6. **Access the application**
   - Open your browser and navigate to `http://localhost:5080` or `https://localhost:7260`

## Database Schema

The application uses Entity Framework Code-First approach with the following main entities:

- **Teams**: Soccer team information
- **Players**: Player details with team associations  
- **Matches**: Match records with results
- **Fixtures**: Upcoming match schedules
- **Venues**: Match venue information
- **Users**: Authentication and authorization data

## Project Structure

```
SoccerPortal/
├── docs/                   # Project documentation
└── SoccerPortal/
   ├── Controllers/         # MVC Controllers
   ├── Models/              # Entity and View Models
   ├── Views/               # Razor Views
   ├── Data/                # Database context and initialization
   ├── Migrations/          # Entity Framework migrations
   ├── Properties/          # Project properties (e.g., launchSettings.json)
   └── wwwroot/             # Static files (CSS, JS, images)
```


## Team Members

- **Emir Beşir** (B2105.090065) - emirbesir@stu.aydin.edu.tr
- **Kerem Taşpınar** (B2105.090084) - keremtaspinar@stu.aydin.edu.tr

## Course Information

- **Course**: Software Architecture
- **Instructor**: Prof. Dr. METİN ZONTUL
- **University**: Istanbul Aydin University
- **Department**: Software Engineering
- **Semester**: Spring 2025

## License

This project is developed for educational purposes as part of the Software Architecture course at Istanbul Aydin University.
