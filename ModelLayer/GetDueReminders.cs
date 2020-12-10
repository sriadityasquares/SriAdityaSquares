using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetDueReminders
    {
        public string ProjectName { get; set; }
        public string TowerName { get; set; }
        public string FlatName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public string AgentName { get; set; }
        public string AgentEmail { get; set; }
        public Nullable<long> AgentMobile { get; set; }
        public string DueAmount { get; set; }
        public string DueDate { get; set; }
    }
}
