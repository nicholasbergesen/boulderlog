using Boulderlog.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Boulderlog.Models
{
    public class ClimbLogViewModel
    {
        public List<string> SessionLabels { get; set; } = new List<string>();
        public List<int> SessionValuesAttempt { get; set; } = new List<int>();
        public List<int> SessionValuesTop { get; set; } = new List<int>();
        public List<int> SessionBoulders { get; set; } = new List<int>();

        public List<double> GradeSuccessRate_Values { get; set; } = new List<double>();
        public List<string> GradeSuccessRate_Label { get; set; } = new List<string>();

        public List<string> GradeRatioAttempt_Label { get; set; } = new List<string>(); 
        public List<double> GradeRatioAttempt_Values { get; set; } = new List<double>();
        public List<double> GradeRatioTop_Values { get; set; } = new List<double>();

        public List<double> GradeAverageAttempt_Values { get; set; } = new List<double>();
        public List<string> GradeAverageAttempt_Label { get; set; } = new List<string>();

        public SelectList Gyms { get; set; }
        public int? GymId { get; set; }
    }
}
