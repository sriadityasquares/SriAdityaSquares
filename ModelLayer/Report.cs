using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Report
    {
        public int Project { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        [Display(Name="Time Period")]
        public int TimePeriod { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }

        public int Type { get; set; }
    }
}
