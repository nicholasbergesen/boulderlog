using Boulderlog.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Boulderlog.Models
{
    public class ClimbViewModel
    {
        public string Id { get; set; }
        public string ImageId { get; set; }
        [DisplayName("Hold Color")]
        public string HoldColor { get; set; }
        public string Wall { get; set; }
        public string UserId { get; set; }
        [DisplayName("Gym")]
        public string Gym { get; set; }
        [DisplayName("Grade")]
        public string Grade { get; set; }
        public int Attempt { get; set; }
        public int Top { get; set; }
        public int Flash { get; set; }
        public string GradeColor { get; internal set; }
    }
}
