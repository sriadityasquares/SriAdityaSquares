using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
   

    public partial class LandlordPayments
    {
        public int PaymentID { get; set; }

        [Required]
        [Display(Name = "Landlord")]
        public Nullable<int> LandlordID { get; set; }
        public string LandlordName { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public string ProjectName { get; set; }

        [Display(Name = "Amount Paid")]
        public Nullable<int> AmountPaid { get; set; }

        [Display(Name = "Payment Date")]
        public Nullable<System.DateTime> PaymentDate { get; set; }

        [Display(Name = "Payment Mode")]
        public string PaymentMode { get; set; }

        [Display(Name = "Reference No")]
        public string ReferenceNo { get; set; }

        public string PayDate { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
