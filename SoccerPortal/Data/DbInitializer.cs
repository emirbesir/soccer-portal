using Microsoft.AspNetCore.Identity;
using SoccerPortal.Models;

namespace SoccerPortal.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SoccerPortalContext context)
        {
            // Make sure the database is created
            context.Database.EnsureCreated();

            // Look for any teams to check if the database has been seeded
            if (context.Teams.Any())
            {
                return; // Database has been seeded
            }

            // Create teams
            var teams = new Team[]
            {
                new Team { TeamName = "Liverpool FC", Coach = "Jürgen Klopp", HomeCity = "Liverpool", FoundedYear = 1892 },
                new Team { TeamName = "Manchester United", Coach = "Erik ten Hag", HomeCity = "Manchester", FoundedYear = 1878 },
                new Team { TeamName = "Chelsea FC", Coach = "Mauricio Pochettino", HomeCity = "London", FoundedYear = 1905 },
                new Team { TeamName = "Arsenal FC", Coach = "Mikel Arteta", HomeCity = "London", FoundedYear = 1886 }
            };
            
            context.Teams.AddRange(teams);
            context.SaveChanges();

            // Create venues
            var venues = new Venue[]
            {
                new Venue { VenueName = "Anfield", Location = "Liverpool, UK" },
                new Venue { VenueName = "Old Trafford", Location = "Manchester, UK" },
                new Venue { VenueName = "Stamford Bridge", Location = "London, UK" },
                new Venue { VenueName = "Emirates Stadium", Location = "London, UK" }
            };
            
            context.Venues.AddRange(venues);
            context.SaveChanges();

            // Create players (just a few examples)
            var players = new Player[]
            {
                new Player { TeamID = teams[0].TeamID, Name = "Mohamed Salah", Position = "Forward", Age = 32, GoalsScored = 186 },
                new Player { TeamID = teams[1].TeamID, Name = "Bruno Fernandes", Position = "Midfielder", Age = 30, GoalsScored = 79 },
                new Player { TeamID = teams[2].TeamID, Name = "Cole Palmer", Position = "Forward", Age = 22, GoalsScored = 25 },
                new Player { TeamID = teams[3].TeamID, Name = "Bukayo Saka", Position = "Forward", Age = 23, GoalsScored = 53 }
            };
            
            context.Players.AddRange(players);
            context.SaveChanges();

            // Create some matches
            var matches = new Match[]
            {
                new Match 
                { 
                    Team1ID = teams[0].TeamID, 
                    Team2ID = teams[1].TeamID, 
                    MatchDate = DateTime.Parse("2025-08-17"), 
                    Score = "", 
                    VenueID = venues[0].VenueID 
                },
                new Match 
                { 
                    Team1ID = teams[2].TeamID, 
                    Team2ID = teams[3].TeamID, 
                    MatchDate = DateTime.Parse("2025-08-17"), 
                    Score = "", 
                    VenueID = venues[2].VenueID 
                },
                new Match 
                { 
                    Team1ID = teams[0].TeamID, 
                    Team2ID = teams[3].TeamID, 
                    MatchDate = DateTime.Parse("2025-05-19"), 
                    Score = "2-1", 
                    VenueID = venues[0].VenueID 
                },
            };
            
            context.Matches.AddRange(matches);
            context.SaveChanges();

            // Create fixtures for the upcoming matches
            var fixtures = new Fixture[]
            {
                new Fixture
                {
                    MatchID = matches[0].MatchID,
                    FixtureDate = DateTime.Parse("2025-08-17 15:00"),
                    Status = "Scheduled"
                },
                new Fixture
                {
                    MatchID = matches[1].MatchID,
                    FixtureDate = DateTime.Parse("2025-08-17 17:30"),
                    Status = "Scheduled"
                            };
            
            context.Fixtures.AddRange(fixtures);
            context.SaveChanges();
        }

        public static async Task SeedRolesAndAdminUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Create roles
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create default admin user
            var adminEmail = "admin@soccerportal.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
