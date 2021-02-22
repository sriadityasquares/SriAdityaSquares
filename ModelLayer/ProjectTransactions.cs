using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ProjectTransactions
    {
        public int ProjectID { get; set; }
        public string TotalSalesAmount { get; set; }
        public string CollectedAmount { get; set; }
        public string BalanceAmount { get; set; }
        public string ProjectName { get; set; }
    }
}
