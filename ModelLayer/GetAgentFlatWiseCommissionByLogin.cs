using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetAgentFlatWiseCommissionByLogin
    {
        public int FlatID { get; set; }
        public string FlatName { get; set; }
        public int AgentID { get; set; }
        public string AgentName { get; set; }
        public Nullable<int> AgentCommission { get; set; }

        public Nullable<int> Advance { get; set; }
        public string Comment { get; set; }
        public Nullable<double> Percentage { get; set; }
        public Nullable<int> AmountPaid { get; set; }
        public Nullable<int> NetBalance { get; set; }
        public Nullable<int> Discount { get; set; }
        public string ProjectName { get; set; }
        public string TowerName { get; set; }
        public Nullable<decimal> AgentTDS { get; set; }
        public Nullable<decimal> AgentGross { get; set; }

        public Nullable<int> BSP { get; set; }

    }
}
