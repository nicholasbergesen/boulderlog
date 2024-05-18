using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace boulderlog.Data.Models
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
        public string Type { get; set; }

        [Required]
        public string ClimbId { get; set; }

        [ForeignKey(nameof(ClimbId))]
        public Climb? Climb { get; set; }
    }
}
