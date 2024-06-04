using System;
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
        [Key, MaxLength(36)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }

        [MaxLength(36), Required]
        [DisplayName("Image")]
        public string? ImageId { get; set; }

        [DataType(DataType.DateTime)]
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(255), Required]
        public required string Grade { get; set; }

        [MaxLength(255)]
        [DisplayName("Hold Color")]
        public string? HoldColor { get; set; }

        [MaxLength(255), Required]
        public required string Gym { get; set; }

        [MaxLength(255)]
        public string? Wall { get; set; }

        public required int GradeId { get; set; }

        public required int GymId { get; set; }

        [MaxLength(36), Required]
        public required string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual AppUser? User { get; set; }

        [ForeignKey(nameof(GradeId))]
        public virtual Grade? GradeCol { get; set; }

        [ForeignKey(nameof(GymId))]
        public virtual Gym? GymCol { get; set; }

        public ICollection<ClimbLog> ClimbLogs { get; set; } = new List<ClimbLog>();
    }
}
