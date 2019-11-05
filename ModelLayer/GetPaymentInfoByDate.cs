using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
        public class GetPaymentInfoByDate
        {
            public string projectname { get; set; }
            public string towername { get; set; }
            public string flatname { get; set; }
            public string name { get; set; }
            public string agentname { get; set; }
            public Nullable<int> BookingAmount { get; set; }
            public Nullable<int> BalanceAmount { get; set; }
            public string PaidDate { get; set; }
            public string DueDate { get; set; }
            public Nullable<int> year { get; set; }
            public Nullable<int> month { get; set; }
            public Nullable<int> day { get; set; }
        }
    
}
