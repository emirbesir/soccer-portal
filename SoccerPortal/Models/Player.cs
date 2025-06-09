using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerPortal.Models
{
    public class Player
    {
        [Key]
        public int PlayerID { get; set; }
        
        [Required]
        public int TeamID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Position { get; set; } = string.Empty;
        
        public int? Age { get; set; }
        
        public int GoalsScored { get; set; }
        
        // Navigation property
        [ForeignKey("TeamID")]
        public virtual Team Team { get; set; } = null!;
    }
}
