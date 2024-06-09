using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net;

namespace Boulderlog.Models
{
    public class FilterViewModel
    {
        public SelectList Grade { get; set; }
        public SelectList Wall { get; set; }
    }
}
