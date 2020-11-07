using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Agreements
    {
        public int AgreementID { get; set; }

        [Display(Name = "Project")]
        public Nullable<int> ProjectID { get; set; }
        public string ProjectName { get; set; }

        [Display(Name = "Tower/Plot")]
        public Nullable<int> TowerID { get; set; }
        public string TowerName { get; set; }

        [Display(Name = "Flat/Villa")]
        public Nullable<int> FlatID { get; set; }
        public string FlatName { get; set; }

        [Display(Name = "Agent")]
        public Nullable<int> AgentID { get; set; }

        
        public string AgentName { get; set; }
        public string Commission { get; set; }
        [Display(Name = "Payment Mode")]
        public string PaymentMode { get; set; }
        
        public string Comments { get; set; }

        [Display(Name = "Agreement Date")]
        public string AgreementDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
