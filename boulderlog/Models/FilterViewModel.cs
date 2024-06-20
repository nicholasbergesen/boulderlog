using Microsoft.AspNetCore.Mvc.Rendering;

namespace Boulderlog.Models
{
    public class FilterViewModel
    {
        public SelectList Grade { get; set; }
        public SelectList Wall { get; set; }
    }
}
