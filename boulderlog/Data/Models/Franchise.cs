using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boulderlog.Data.Models
{
    [Table("Franchise")]
    public class Franchise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Id { get; set; }

        [MaxLength(255)]
        public required string Name { get; set; }

        public ICollection<Gym>? Gym { get; set; }

        public ICollection<Grade>? Grade { get; set; }
    }
}
