using Boulderlog.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Boulderlog.Models
{
    public class ClimbLogModel
    {
        public string? Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public required string Type { get; set; }

        [Required]
        public required string ClimbId { get; set; }

        [ForeignKey(nameof(ClimbId))]
        public Climb? Climb { get; set; }
    }
}
