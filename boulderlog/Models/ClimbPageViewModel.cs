using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Boulderlog.Models
{
    public class ClimbPageViewModel
    {
        public List<ClimbViewModel> ClimbViewModels { get; set; }
        public SelectList Gyms { get; set; }
        public int SelectedGymId { get; set; }
    }
}
