using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boulderlog.Data.Models
{
    [Table("Grade")]

    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public required int Id { get; set; }

        [MaxLength(7)]
        public required string ColorHex { get; set; }

        [MaxLength(25)]
        public required string ColorName { get; set; }

        public int SortOrder { get; set; }

        [MaxLength(3)]
        public required string VScale { get; set; }

        [MaxLength(36)]
        public int GymId { get; set; }

        [ForeignKey(nameof(GymId))]
        public virtual Gym? Gym { get; set; }
    }
}
