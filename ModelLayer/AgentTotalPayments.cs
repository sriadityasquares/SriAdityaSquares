using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class AgentTotalPayments
    {
        
        public int AgentTotalPayID { get; set; }

        [Display(Name = "Agents")]
        public Nullable<int> AgentID { get; set; }

        [Display(Name = "New Paid")]
        public Nullable<int> AmountPaid { get; set; }
        [Display(Name = "Agent Commission")]
        public Nullable<int> AgentCommission { get; set; }

        [Display(Name = "Total Paid")]
        public Nullable<int> TotalPaid { get; set; }

        [Display(Name = "Total Balance")]
        public Nullable<int> TotalBalance { get; set; }
        [Display(Name = "Paid Date")]
        public Nullable<System.DateTime> PaidDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
