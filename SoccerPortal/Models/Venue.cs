using System.ComponentModel.DataAnnotations;

namespace SoccerPortal.Models
{
    public class Venue
    {
        [Key]
        public int VenueID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string VenueName { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;
        
        // Navigation property
        public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
