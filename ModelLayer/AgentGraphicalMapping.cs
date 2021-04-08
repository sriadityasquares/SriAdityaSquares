using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class AgentGraphicalMapping
    {
        public int AgentID { get; set; }
        public string AgentName { get; set; }
        public Nullable<long> AgentMobileNo { get; set; }
        public string AgentCode { get; set; }
        public Nullable<int> AgentCommission { get; set; }
        public string AgentSponserCode { get; set; }
        public Nullable<int> AmountPaid { get; set; }
        public Nullable<int> NetBalance { get; set; }
        public string Designation { get; set; }

    }
}
