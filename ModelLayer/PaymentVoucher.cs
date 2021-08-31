using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class PaymentVoucher
    {
        public string VoucherNo { get; set; }

        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }


        [Display(Name = "Quotation No")]
        public string QuotationNo { get; set; }

        [Display(Name = "Supplier")]
        public string SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }

        [Display(Name = "Payment Date")]
        public Nullable<System.DateTime> PaymentDate { get; set; }

        [Display(Name = "Payment Date")]
        public string PaidDate { get; set; }

        [Display(Name = "Payment Mode")]
        public string ModeOfPayment { get; set; }

        [Display(Name = "Reference No")]
        public string ReferenceNo { get; set; }

        [Display(Name = "Amount Paid")]
        public Nullable<int> AmountPaid { get; set; }
        [Display(Name = "TDS")]
        public Nullable<double> TDS { get; set; }

        [Display(Name = "Total Paid")]
        public Nullable<int> TotalPaid { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public string PrintVoucher { get; set; }
    }
}
