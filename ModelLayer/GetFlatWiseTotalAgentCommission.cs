using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public partial class GetFlatWiseTotalAgentCommission
    {
        public Nullable<int> FlatID { get; set; }
        public string FlatName { get; set; }
        public Nullable<int> FinalRate { get; set; }
        public int AgentID { get; set; }
        public string AgentName { get; set; }
        public Nullable<double> AgentComm { get; set; }
        public string AgentParent { get; set; }
    }
}
