using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    

    public class Cheques
    {
        public int ID { get; set; }


        [Display(Name = "Cheque No")]
        public string ChequeNumber { get; set; }

        [Display(Name = "Cheque Date")]
        public Nullable<System.DateTime> ChequeDate { get; set; }

        [Display(Name = "Cheque Date")]
        public string ChequeDateString { get; set; }

        [Display(Name = "Cheque Status")]
        public string Status { get; set; }
        public Nullable<int> Amount { get; set; }
        public string Category { get; set; }

        [Display(Name = "IBO")]
        public Nullable<int> IBOID { get; set; }
        public string PaidTo { get; set; }

        public string Remarks { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
