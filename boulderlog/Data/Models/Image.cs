using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boulderlog.Data.Models
{
    [Table("Image")]
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }

        [Required, MaxLength(255)]
        public required string FileType { get; set; }

        [Required, MaxLength(400)]
        public required string FileName { get; set; }

        [Required]
        public required byte[] Bytes { get; set; }
    }
}
