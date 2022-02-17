using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetCancelledFlatsInfo
    {

        public Nullable<int> FlatID { get; set; }
        public string ProjectName { get; set; }
        public string TowerName { get; set; }
        public string FlatName { get; set; }
        public string CustomerName { get; set; }
        public Nullable<int> Area { get; set; }
        public Nullable<int> SftRate { get; set; }
        public Nullable<int> Discount { get; set; }
        public string AgentName { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public Nullable<int> GrandRate { get; set; }
        public Nullable<int> BookingAmount { get; set; }
        public Nullable<int> PaymentID { get; set; }
        public Nullable<System.DateTime> PaidDate { get; set; }
        public Nullable<int> Payments { get; set; }
        public Nullable<int> PaymentBalance { get; set; }
    }
}
