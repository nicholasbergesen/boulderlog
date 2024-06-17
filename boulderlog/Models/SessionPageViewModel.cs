using Boulderlog.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Boulderlog.Models
{
    public class SessionPageViewModel
    {
        public List<ClimbViewModel> ClimbViewModels { get; set; }
        public SelectList Gyms { get; set; }
        public int SelectedGymId { get; set; }
        public SessionFilter SessionFilter { get; set; }
    }
}
