using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetAgentWiseBookingDetails
    {
        public Nullable<int> AgentID { get; set; }
        public string AgentName { get; set; }
        public Nullable<int> TotalBookingamount { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Day { get; set; }
        public Nullable<int> flatsbooked { get; set; }
        public Nullable<int> projectid { get; set; }
        public Nullable<int> towerid { get; set; }
        public string projectname { get; set; }
        public string towername { get; set; }
    }
}
