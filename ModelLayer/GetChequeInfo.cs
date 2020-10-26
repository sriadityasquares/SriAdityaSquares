using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetChequeInfo
    {
        public int PaymentID { get; set; }
        public Nullable<System.Guid> BookingID { get; set; }
        public Nullable<int> FlatID { get; set; }
        public Nullable<int> BookingAmount { get; set; }
        public Nullable<int> BalanceAmount { get; set; }
        public string PaymentModeID { get; set; }
        public string PaymentMode { get; set; }
        public string ReferenceNo { get; set; }
        public Nullable<int> Aadhar { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Day { get; set; }
        public string ChequeStatus { get; set; }
        public Nullable<System.DateTime> ChequeDate { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string ProjectName { get; set; }
        public string TowerName { get; set; }
        public string FlatName { get; set; }
    }
}
