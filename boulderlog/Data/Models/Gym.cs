using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boulderlog.Data.Models
{
    [Table("Gym")]
    public class Gym
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Id { get; set; }

        [MaxLength(255)]
        public required string Name { get; set; }

        [MaxLength(1000)]
        public required string Walls { get; set; }

        public int? FranchiseId { get; set; }

        [ForeignKey(nameof(FranchiseId))]
        public Franchise? Franchise { get; set; }
    }
}
