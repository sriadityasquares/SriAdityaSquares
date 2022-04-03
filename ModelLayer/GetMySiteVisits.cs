using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetMySiteVisits
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string AgentName { get; set; }
        public string ImmediateSeniorName { get; set; }
        public string CustomerName { get; set; }
        public Nullable<int> NoOfCustomers { get; set; }
        public string CustomerMobile { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string DateOfVisit { get; set; }
        public string Status { get; set; }

        public string AgentMobile { get; set; }
    }
}
