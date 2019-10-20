using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public partial class PaymentInfo
    {
        public int PaymentID { get; set; }
        public Nullable<System.Guid> BookingID { get; set; }
        public Nullable<int> BookingAmount { get; set; }
        public Nullable<int> BalanceAmount { get; set; }
        public Nullable<int> PaymentMode { get; set; }
        public string ReferenceNo { get; set; }
        public Nullable<int> Aadhar { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
