using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class EligibleFlatsForCommission
    {
        public int FlatID { get; set; }
        public string Project { get; set; }
        public string Tower { get; set; }
        public string Flat { get; set; }
        public string Customer { get; set; }
        public string Agent { get; set; }
        public string Scheme { get; set; }
        public string SalePrice { get; set; }
        public Nullable<int> Paid { get; set; }
        public Nullable<int> Balance { get; set; }

        public Nullable<int> Sft { get; set; }
    }
}
