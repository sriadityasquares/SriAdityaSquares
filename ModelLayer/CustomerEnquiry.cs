using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class CustomerEnquiry
    {
        [Display(Name ="Name")]
        public string CustomerName { get; set; }

        public Nullable<long> Mobile { get; set; }
        public string Enquiry { get; set; }
        [Display(Name = "Project")]
        public Nullable<int> ProjectID { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
