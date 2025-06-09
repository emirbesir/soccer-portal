# Soccer Portal

ASP.NET MVC Project for viewing soccer teams, players, match results.

## Authentication

The application now uses ASP.NET Core Identity for user management. A default
admin user is seeded on startup:

- **Email:** `admin@example.com`
- **Password:** `Admin@123`

Admin users can create, edit, or delete data. Anonymous users may only view
information.
