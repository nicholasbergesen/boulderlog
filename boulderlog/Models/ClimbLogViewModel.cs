using System.Collections.Generic;

namespace Boulderlog.Models
{
    public class ClimbLogViewModel
    {
        public List<string> SessionLabels { get; set; } = new List<string>();
        public List<int> SessionValuesAttempt { get; set; } = new List<int>();
        public List<int> SessionValuesTop { get; set; } = new List<int>();
        public List<int> SessionBoulders { get; set; } = new List<int>();
    }
}
