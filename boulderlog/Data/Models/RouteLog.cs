using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace boulderlog.Data.Models
{
    [Table("RouteLog")]
    public class RouteLog
    {
        [Key]
        public string Id { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        [MaxLength(255), Required]
        public string Type { get; set; }
    }
}
