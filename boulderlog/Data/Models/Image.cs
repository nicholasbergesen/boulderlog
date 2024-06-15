using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boulderlog.Data.Models
{
    [Table("Image")]
    public class Image
    {
        [Key, MaxLength(36)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; }

        [Required, MaxLength(255)]
        public required string FileType { get; set; }

        [Required]
        public required byte[] Bytes { get; set; }
    }
}
