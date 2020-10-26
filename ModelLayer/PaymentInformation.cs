using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class PaymentInformation
    {
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Display(Name = "Tower")]
        public int TowerID { get; set; }
        public int PaymentID { get; set; }
        public Nullable<System.Guid> BookingID { get; set; }
        [Display(Name = "Flat")]
        public Nullable<int> FlatID { get; set; }

        [Display(Name ="New Paid Amount")]
        public Nullable<int> BookingAmount { get; set; }

        [Display(Name ="New Balance Amount")]
        public Nullable<int> BalanceAmount { get; set; }
        [Display(Name = "Mode")]
        public string PaymentModeID { get; set; }
        public string PaymentMode { get; set; }
        public string ReferenceNo { get; set; }
        public Nullable<int> Aadhar { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [Display(Name ="Payment Date")]
        public string PaymentDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public string FormattedDate { get; set; }

        [Display(Name = "Total Paid")]
        public Nullable<int> TotalPaid { get; set; }

        [Display(Name = "Total Balance")]
        public Nullable<int> TotalBalance { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Day { get; set; }

        [Display(Name = "Cheque Status")]
        public string ChequeStatus { get; set; }

        [Display(Name = "Cheque Date")]
        public Nullable<System.DateTime> ChequeDate { get; set; }
    }
}
