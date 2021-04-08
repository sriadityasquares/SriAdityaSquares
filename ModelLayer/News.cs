using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class NewsDetails
    {
        public int NewsID { get; set; }
        public string News { get; set; }

        [Display(Name = "News Date")]
        public string NewsDate { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
