using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ClientPayments
    {
        public int ClientPayID { get; set; }

        [Display(Name = "Client")]
        public string ClientName { get; set; }
        [Display(Name = "Project")]
        public Nullable<int> ProjectID { get; set; }
        public string ProjectName { get; set; }

        [Display(Name = "Tower/Plot")]
        public Nullable<int> TowerID { get; set; }
        public string TowerName { get; set; }

        [Display(Name = "Flat/Villa")]
        public Nullable<int> FlatID { get; set; }
        public string FlatName { get; set; }
        [Display(Name = "Amount Received")]
        public int Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public string PaymentMode { get; set; }

        [Display(Name = "Payment Date")]
        public string PaymentDate { get; set; }

        
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
