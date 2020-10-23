using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetFlatPaymentDetails
    {
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string ProjectName { get; set; }
        public string TowerName { get; set; }
        public string FlatName { get; set; }
        public string AgentName { get; set; }
        public Nullable<int> Sft { get; set; }
        public Nullable<int> FinalRate { get; set; }
        public Nullable<int> Discount { get; set; }
        public Nullable<int> GrandRate { get; set; }
        public string SchemeName { get; set; }
        public Nullable<int> TotalPaid { get; set; }
        public Nullable<int> TotalBalance { get; set; }
        public Nullable<System.DateTime> LastPaymentDate { get; set; }
    }
}
