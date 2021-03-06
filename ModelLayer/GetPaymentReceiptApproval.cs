using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetPaymentReceiptApproval
    {
        public int PaymentID { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string ProjectName { get; set; }
        public string TowerName { get; set; }
        public Nullable<int> FranchiseID { get; set; }
        public string AgentName { get; set; }
        public Nullable<System.DateTime> ChequeDate { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string FlatName { get; set; }
        public Nullable<int> BookingAmount { get; set; }
        public Nullable<double> Bhk { get; set; }
        public Nullable<int> Area { get; set; }
        public string PaymentMode { get; set; }
        public Nullable<int> BalanceAmount { get; set; }
        public string PaymentMode1 { get; set; }
        public string ReferenceNo { get; set; }
        public string Details { get; set; }
        public Nullable<bool> ViewReceipt { get; set; }
    }
}
