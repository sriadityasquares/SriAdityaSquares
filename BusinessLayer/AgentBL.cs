using DataLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class AgentBL
    {
        AgentDL agent = new AgentDL();
        public bool UpdateAgent(AgentMaster a)
        {
            return agent.UpdateAgent(a);
        }

        public bool AddAgent(AgentMaster a)
        {
            return agent.AddAgent(a);
        }
    }
}
