using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace boulderlog.Data.Models
{
    [Table("Route")]
    public class Route
    {
        [Key]
        public string Id { get; set; }

        [MaxLength(255), Required]
        public string Color { get; set; }

        [MaxLength(255), Required]
        public string Gym { get; set; }

        [MaxLength(255)]
        public string Wall { get; set; }

        public ICollection<RouteLog> BoulderLogs { get; set; } = new List<RouteLog>();
    }
}
