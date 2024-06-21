using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Boulderlog.Models
{
    public class CreateClimbViewModel
    {
        [DisplayName("Image"), Required]
        public string? ImageId { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }
        [DisplayName("Hold Color"), Required]
        public string? HoldColor { get; set; }
        [Required]
        public string Wall { get; set; }
        [Required]
        public string CreatedByUserId { get; set; }
        [DisplayName("Gym"), Required]
        public int? GymId { get; set; }
        [DisplayName("Grade"), Required]
        public int? GradeId { get; set; }
        public string? RedirectToAction { get; set; }
    }
}
