using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetLandlordPayments
    {
        public Nullable<int> ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> TotalAmount { get; set; }
        public Nullable<int> TotalPaid { get; set; }
        public Nullable<int> TodayPayment { get; set; }
        public Nullable<int> Balance { get; set; }
    }
}
