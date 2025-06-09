# Istanbul Aydin University

# Department of Software Engineering

## Course:

```
Software Architecture
```
## Instructor:

```
Prof. Dr. METİN ZONTUL
```
## Semester/Year:

```
Spring 2025
```
## Project Title:

```
Soccer Results Portal
```
## Team Members:

- Student 1: Emir Beşir, B2105.090065, emirbesir@stu.aydin.edu.tr
- Student 2: Kerem Taşpınar, B2105.090084, keremtaspinar@stu.aydin.edu.tr


# Table of Contents

1. Abstract

2. Introduction

3. Project Objectives

4. System Overview

o 4.1 Functional Requirements

o 4.2 Non-Functional Requirements

5. Software Architecture

o 5.1 Architectural Pattern (MVC)

6. Database Design

o 6.1 Database Overview

o 6.2 Tables and Relationships

7. Technology Stack

o 7.1 Front-End (Bootstrap)

o 7.2 Back-End (ASP.NET)

o 7.3 Database (Microsoft SQL)

o 7.4 Code-First Approach

8. Conclusion


## 1. Abstract

This project proposal outlines the development of a Soccer Results Portal, a web-based sys-
tem designed to display soccer team match results and fixtures. The system will provide an
easy-to-use interface for users to view upcoming matches, past results, and team details. It
will be developed using a multi-layer architecture with ASP.NET Core as the back-end
framework, Bootstrap for the front end, and Microsoft SQL Server for the database. The
MVC (Model-View-Controller) pattern will be implemented to ensure maintainability and
scalability.

## 2. Introduction

The Soccer Results Portal is aimed at providing an intuitive platform where soccer enthusi-
asts can check match fixtures, team details, and results. The website will support CRUD
(Create, Read, Update, Delete) operations for matches, teams, and fixtures, ensuring that
authorized users can manage content dynamically. The system will be developed following
software engineering best practices to achieve an efficient, secure, and scalable solution.


## 3. Project Objectives

- Develop a user-friendly and responsive web-based portal for soccer match results.
- Implement a multi-layer software architecture using ASP.NET Core and SQL
    Server.
- Use the MVC design pattern to maintain a clear separation between the UI, business
    logic, and data access layers.
- Ensure data integrity and normalization by designing an optimized relational data-
    base.
- Provide administrative functionality for managing teams, fixtures, and results.

## 4. System Overview

4.1 Functional Requirements

- Display a list of upcoming and past soccer matches.
- Provide detailed information about teams, including players and match history.
- Allow administrators to add, update, and delete match records.
- Implement a search functionality to filter matches by date, team, or venue.
- Enable secure login and role-based access for admins.

4.2 Non-Functional Requirements

- The system must be responsive.
- Data must be stored securely and follow 1NF, 2NF, and 3NF normalization.
- The system should handle at least 100 concurrent users.
- Implement authentication and authorization mechanisms for data protection.


## 5. Software Architecture

5.1 Architectural Pattern (MVC)

- Model: Represents the data layer, interacting with the database using Entity Frame-
    work.
- View: The front-end user interface, designed with Bootstrap for a clean and respon-
    sive layout.
- Controller: Handles user requests, processes data, and communicates with the model
    and view.

This structure ensures modularity, scalability, and ease of maintenance.

## 6. Database Design

6.1 Database Overview

The database is designed using Microsoft SQL Server with at least five tables to manage
soccer match data efficiently. The schema follows data normalization rules to eliminate re-
dundancy and ensure consistency.

6.2 Tables and Relationships

1. Teams Table (TeamID, TeamName, Coach, HomeCity, FoundedYear)
2. Players Table (PlayerID, TeamID, Name, Position, Age, GoalsScored)
3. Matches Table (MatchID, Team1ID, Team2ID, MatchDate, Score, VenueID)
4. Fixtures Table (FixtureID, MatchID, FixtureDate, Status)
5. Venues Table (VenueID, VenueName, Location)


## 7. Technology Stack

7.1 Front-End (Bootstrap)

- HTML, CSS, JavaScript
- Bootstrap framework for responsive UI design
- Pre-built components that reduce development time.

7.2 Back-End (ASP.NET Core)

- ASP.NET Core offers a framework for creating web applications.
- It has integrated support for MVC, making the presentation, business logic and data
    access separate.

7.3 Database (Microsoft SQL Server)

- Microsoft SQL Server for secure and scalable data storage
- SQL Server Management Studio (SSMS) for database administration

7.4 Code-First Approach

- Entity Framework Core will be used for database management.
- Code-first migration will allow dynamic table creation from C# classes.

## 8. Conclusion

This project aims to provide a comprehensive and user-friendly web portal for displaying
soccer match results and fixtures. By following a multi-layer architecture with ASP.NET
Core and Microsoft SQL Server, the system ensures scalability, security, and maintainabil-
ity. The MVC pattern will be implemented to enhance code organization and ease future
modifications. The database will be structured following data normalization rules to main-
tain integrity and optimize performance.

Upon successful completion, the Soccer Results Portal will serve as a valuable platform for
soccer fans and administrators to track matches, teams, and statistics efficiently.


