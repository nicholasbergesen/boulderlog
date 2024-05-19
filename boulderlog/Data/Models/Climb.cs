using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boulderlog.Data.Models
{
    // The name Route conflicts with the asp.net Route class
    [Table("Climb")]
    public class Climb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }

        [MaxLength(255), Required]
        public string Name { get; set; }

        [MaxLength(255), Required]
        public string Grade { get; set; }

        [MaxLength(255)]
        [DisplayName("Hold Color")]
        public string? HoldColor { get; set; }

        [MaxLength(255), Required]
        public string Gym { get; set; }

        [MaxLength(255)]
        public string? Wall { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual AppUser? User { get; set; }

        public ICollection<ClimbLog> ClimbLogs { get; set; } = new List<ClimbLog>();
    }
}
