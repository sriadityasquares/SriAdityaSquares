using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetFlatLifeCycle
    {

        public Nullable<int> BookingAmount { get; set; }
        public Nullable<int> BalanceAmount { get; set; }
        public string CreatedDate { get; set; }

        public string FlatEvent { get; set; }
        public string Name { get; set; }
        public string SchemeName { get; set; }

        public double PercentageCompleted { get; set; }

    }
}
