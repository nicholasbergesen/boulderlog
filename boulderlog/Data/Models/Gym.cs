using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Boulderlog.Data.Models
{
    [Table("Gym")]
    public class Gym
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public required int Id { get; set; }

        [MaxLength(255)]
        public required string Name { get; set; }

        [MaxLength(1000)]
        public required string Walls { get; set; }
    }
}
