using Boulderlog.Data.Models;
using System.Collections.Generic;

namespace Boulderlog.Models
{
    public class ClimbViewModel
    {
        public ClimbViewModel(Climb climb) 
        {
            this.Climb = climb;
        }

        public Climb Climb { get; set; }
        public int Attempt { get; set; }
        public int Top { get; set; }
        public int Flash { get; set; }
    }
}
