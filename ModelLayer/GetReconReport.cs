using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    

    public class GetReconReport
    {
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }

        [Display(Name = "TOTAL CREDIT")]
        public Nullable<double> CreditAmount { get; set; }
        public Nullable<System.DateTime> VoucherDate { get; set; }
        public string VoucherNo { get; set; }

        [Display(Name = "TOTAL DEBIT")]
        public Nullable<int> DebitAmount { get; set; }
        public string ModeOfPayment { get; set; }
        public string ReferenceNo { get; set; }
        public string QuotationNo { get; set; }

        [Display(Name = "Supplier")]
        public string SupplierID { get; set; }

        [Display(Name = "Phone No")]
        public string SupplierPhone { get; set; }

        [Display(Name = "BALANCE")]
        public Nullable<double> BalanceAmount { get; set; }
    }
}
