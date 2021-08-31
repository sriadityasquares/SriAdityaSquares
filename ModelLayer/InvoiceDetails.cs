using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class InvoiceDetails
    {
        public Guid ID { get; set; }
        public string InvoiceNo { get; set; }
        public string QuotationNo { get; set; }
        public Nullable<int> SerialNo { get; set; }
        public string Description { get; set; }
        public Nullable<double> UnitPrice { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> Amount { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
