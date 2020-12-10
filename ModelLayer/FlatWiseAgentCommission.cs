using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class FlatWiseAgentCommission
    {
        public int? FlatID { get; set; }
        public string FlatName { get; set; }
        public int? AgentID { get; set; }
        public string AgentName { get; set; }
        public Nullable<int> AgentCommission { get; set; }

        public double? Percentage { get; set; }

        public string AgentCode { get; set; }
        //public Nullable<int> AgentCommission { get; set; }
        public string AgentSponserCode { get; set; }

        public int AmountPaid { get; set; }
        public Nullable<int> Discount { get; set; }
        public int NetBalance { get; set; }

        public Nullable<int> IBOShare { get; set; }
    }
}
