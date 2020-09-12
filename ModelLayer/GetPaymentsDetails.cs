using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetPaymentsDetails
    {
        public int PaymentID { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string ProjectName { get; set; }
        public string TowerName { get; set; }
        public string Name { get; set; }
        public Nullable<int> Mobile { get; set; }
        public string FlatName { get; set; }
        public Nullable<int> BookingAmount { get; set; }
        public Nullable<int> Bhk { get; set; }
        public Nullable<int> Area { get; set; }
        public string PaymentMode { get; set; }
        public string ReferenceNo { get; set; }
    }
}
