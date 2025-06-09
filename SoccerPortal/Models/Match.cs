using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerPortal.Models
{
    public class Match
    {
        [Key]
        public int MatchID { get; set; }
        
        [Required]
        public int Team1ID { get; set; }
        
        [Required]
        public int Team2ID { get; set; }
        
        public DateTime MatchDate { get; set; }
        
        [StringLength(20)]
        public string Score { get; set; } = string.Empty;
        
        public int VenueID { get; set; }
        
        // Navigation properties
        [ForeignKey("Team1ID")]
        public virtual Team Team1 { get; set; } = null!;
        
        [ForeignKey("Team2ID")]
        public virtual Team Team2 { get; set; } = null!;
        
        [ForeignKey("VenueID")]
        public virtual Venue Venue { get; set; } = null!;
        
        public virtual ICollection<Fixture> Fixtures { get; set; } = new List<Fixture>();
    }
}
