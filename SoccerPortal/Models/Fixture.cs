using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerPortal.Models
{
    public class Fixture
    {
        [Key]
        public int FixtureID { get; set; }
        
        [Required]
        public int MatchID { get; set; }
        
        public DateTime FixtureDate { get; set; }
        
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;
        
        // Navigation property
        [ForeignKey("MatchID")]
        public virtual Match Match { get; set; } = null!;
    }
}
