﻿using Boulderlog.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Boulderlog.Models
{
    public class FilterViewModel
    {
        public IEnumerable<ColorDTO> Grade { get; set; }
        public SelectList Wall { get; set; }
    }

    public class ColorDTO 
    {
        public int Id { get; set; }
        public string ColorHex { get;set; }
        public string ColorName { get; set; }
    }
}
