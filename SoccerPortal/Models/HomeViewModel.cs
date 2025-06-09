using SoccerPortal.Models;

namespace SoccerPortal.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Match> UpcomingMatches { get; set; } = new List<Match>();
        public IEnumerable<Match> PastMatches { get; set; } = new List<Match>();
    }
}
