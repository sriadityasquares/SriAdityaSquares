using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class PastDue
    {
        public string ProjectName { get; set; }
        public string TowerName { get; set; }
        public string FlatName { get; set; }
        public string AgentName { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<int> DueAmount { get; set; }
        public System.Guid bookingID { get; set; }
    }
}
