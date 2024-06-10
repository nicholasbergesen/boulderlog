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

        [Required, DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        [MaxLength(255), Required]
        public string GradeOld { get; set; }

        [MaxLength(255)]
        [DisplayName("Hold Color")]
        public string? HoldColor { get; set; }

        [MaxLength(255), Required]
        public string GymOld { get; set; }

        [MaxLength(255)]
        public string? Wall { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual AppUser? User { get; set; }

        [DisplayName("Gym")]
        public int? GymId { get; set; }

        [ForeignKey(nameof(GymId))]
        public virtual Gym? Gym { get; set; }

        [DisplayName("Grade")]
        public int? GradeId { get; set; }

        [ForeignKey(nameof(GradeId))]
        public virtual Grade? Grade { get; set; }

        public ICollection<ClimbLog> ClimbLogs { get; set; } = new List<ClimbLog>();
    }
}