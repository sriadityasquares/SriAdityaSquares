using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Suppliers
    {
        public string SupplierID { get; set; }

        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }


        [Display(Name = "Supplier Phone")]
        public string SupplierPhone { get; set; }
        public string GSTIN { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
