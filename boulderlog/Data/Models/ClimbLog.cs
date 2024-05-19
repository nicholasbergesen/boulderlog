using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boulderlog.Data.Models
{
    [Table("ClimbLog")]
    public class ClimbLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        [MaxLength(255), Required]
        public required string Type { get; set; }

        [Required]
        public required string ClimbId { get; set; }

        [ForeignKey(nameof(ClimbId))]
        public Climb? Climb { get; set; }
    }
}
