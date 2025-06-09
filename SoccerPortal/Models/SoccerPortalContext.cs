using Microsoft.EntityFrameworkCore;

namespace SoccerPortal.Models
{
    public class SoccerPortalContext : DbContext
    {
        public SoccerPortalContext(DbContextOptions<SoccerPortalContext> options) : base(options)
        {
        }
        
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Team and Player
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamID)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configure the relationship between Team and Match (Team1)
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Team1)
                .WithMany()
                .HasForeignKey(m => m.Team1ID)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configure the relationship between Team and Match (Team2)
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Team2)
                .WithMany()
                .HasForeignKey(m => m.Team2ID)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configure the relationship between Venue and Match
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Venue)
                .WithMany(v => v.Matches)
                .HasForeignKey(m => m.VenueID)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configure the relationship between Match and Fixture
            modelBuilder.Entity<Fixture>()
                .HasOne(f => f.Match)
                .WithMany(m => m.Fixtures)
                .HasForeignKey(f => f.MatchID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
