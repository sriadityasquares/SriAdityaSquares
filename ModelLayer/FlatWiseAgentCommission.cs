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
    }
}
