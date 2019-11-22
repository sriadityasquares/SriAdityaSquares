using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Schemes
    {
        public int SchemeID { get; set; }
        public int ProjectID { get; set; }
        public string SchemeName { get; set; }
        public string SchemeStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> SqFtCost { get; set; }
        public Nullable<int> PaymentTimePeriod { get; set; }
            public Nullable<int> PaymentPercentage { get; set; }
        public Nullable<int> DownPayment { get; set; }
        public Nullable<int> DownPaymentTimePeriod { get; set; }
    }
}
