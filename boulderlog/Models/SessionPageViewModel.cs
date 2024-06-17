using Boulderlog.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Boulderlog.Models
{
    public class SessionPageViewModel
    {
        public Dictionary<string, List<ClimbViewModel>> ClimbViewModels { get; set; }
        public SessionFilter SessionFilter { get; set; }
    }
}
