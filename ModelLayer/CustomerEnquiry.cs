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

        public int EnquiryID { get; set; }
        [Display(Name ="Name")]
        public string CustomerName { get; set; }

        public Nullable<long> Mobile { get; set; }

        [StringLength(100, MinimumLength = 0)]
        public string Enquiry { get; set; }
        [Display(Name = "Project")]
        public Nullable<int> ProjectID { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Sms { get; set; }

        public string Recipients { get; set; }

        public Nullable<System.DateTime> EnquiryDate { get; set; }

    }
}
