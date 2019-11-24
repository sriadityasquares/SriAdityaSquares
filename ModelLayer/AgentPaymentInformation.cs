using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class AgentPaymentInformation
    {
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Display(Name = "Tower")]
        public int TowerID { get; set; }
        public int AgentPaymentID { get; set; }
        public Nullable<System.Guid> BookingID { get; set; }
        [Display(Name = "Flat")]
        public Nullable<int> FlatID { get; set; }
        [Display(Name = "Agent New Paid")]
        public Nullable<double> AgentNetPaid { get; set; }
        [Display(Name = "Agent New Balance")]
        public Nullable<double> AgentNetBalance { get; set; }
        [Display(Name = "SAS New Paid")]
        public Nullable<double> SASNetPaid { get; set; }
        [Display(Name = "SAS New Balance")]
        public Nullable<double> SASNetBalance { get; set; }

        [Display(Name = "Agent Net Paid")]
        public Nullable<int> TotalPaidAgent { get; set; }

        [Display(Name = "Agent Net Balance")]
        public Nullable<int> TotalBalanceAgent { get; set; }

        [Display(Name = "SAS Net Paid")]
        public Nullable<int> TotalPaidSAS { get; set; }

        [Display(Name = "SAS Net Balance")]
        public Nullable<int> TotalBalanceSAS { get; set; }

        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public string FormattedDate { get; set; }

        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Day { get; set; }
    }
}
