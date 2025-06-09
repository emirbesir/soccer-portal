using System.ComponentModel.DataAnnotations;

namespace SoccerPortal.Models
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string TeamName { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string Coach { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string HomeCity { get; set; } = string.Empty;
        
        public int? FoundedYear { get; set; }
        
        // Navigation properties
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
