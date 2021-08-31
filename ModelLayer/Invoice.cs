using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Invoice
    {

        public int ID { get; set; }

        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }

        [Display(Name = "Quotation No")]
        [Required]
        public string QuotationNo { get; set; }

        [Display(Name = "Supplier")]
        [Required]
        public string SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }

        [Display(Name = "Expense Type")]
        [Required]
        public string ExpenseType { get; set; }

        [Display(Name = "Project")]
        public Nullable<int> ProjectID { get; set; }
        public string ProjectName { get; set; }

        [Display(Name = "Invoice Date")]
        public Nullable<System.DateTime> InvoiceDate { get; set; }

        [Display(Name = "Total")]
        public Nullable<double> TotalValue { get; set; }

        [Display(Name = "CGST %")]
        public Nullable<double> CGSTPercentage { get; set; }
        public Nullable<double> CGST { get; set; }

        [Display(Name = "SGST %")]
        public Nullable<double> SGSTPercentage { get; set; }
        public Nullable<double> SGST { get; set; }

        [Display(Name = "IGST %")]
        public Nullable<double> IGSTPercentage { get; set; }
        public Nullable<double> IGST { get; set; }

        [Display(Name = "Net")]
        public Nullable<double> NetValue { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Required]
        public string InvoiceDetails { get; set; }

        public string InvDate { get; set; }

    }
}
