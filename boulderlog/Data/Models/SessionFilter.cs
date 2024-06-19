using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace Boulderlog.Data.Models
{
    public class SessionFilter
    {
        [Key]
        public int? Id { get; set; }

        [MaxLength(36)]
        public required string UserId { get; set; }
        public int? GymId { get; set; }
        public int? GradeId { get; set; }

        [MaxLength(255)]
        public string? Wall { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime LastUpdated { get; set; }

        [ForeignKey(nameof(GymId))]
        public virtual Gym? Gym { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual AppUser? User { get; set; }

        [ForeignKey(nameof(GradeId))]
        public virtual Grade? Grade { get; set; }
    }
}
