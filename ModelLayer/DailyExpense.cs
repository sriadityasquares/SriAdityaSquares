using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class DailyExpense
    {
        public int ExpenseID { get; set; }

        [Display(Name = "Expense Date")]
        public string ExpenseDate { get; set; }
        public string Particulars { get; set; }
        public Nullable<int> Amount { get; set; }

        [Display(Name = "Paid To")]
        public string PaidTo { get; set; }

        [Display(Name = "Payment Mode")]
        public string PaymentMode { get; set; }
        public string Comments { get; set; }

        [Display(Name = "Transaction")]
        public string TransactionType { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
