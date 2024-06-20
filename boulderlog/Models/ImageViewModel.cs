﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Boulderlog.Models
{
    public class ImageViewModel
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; }

        [Required, MaxLength(255)]
        public required string FileType { get; set; }
    }
}
