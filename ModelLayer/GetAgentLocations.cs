using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetAgentLocations
    {
        
            public int AgentID { get; set; }
            public string AgentName { get; set; }
            public Nullable<long> AgentMobileNo { get; set; }
            public Nullable<double> AgentLatitude { get; set; }
            public Nullable<double> AgentLongitude { get; set; }
        
    }
}
