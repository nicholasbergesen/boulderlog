using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boulderlog.Models
{
    public class ArchiveViewModel
    {
        public List<ClimbViewModel> ClimbViewModels { get; set; }
        public SelectList Gyms { get; set; }
        public int SelectedGymId { get; set; }
        [DataType(DataType.Date)]
        public DateTime To { get; internal set; }
    }
}
