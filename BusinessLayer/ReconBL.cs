using DataLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ReconBL
    {
        ReconDL db = new ReconDL();
        public bool AddSuppliers(Suppliers s)
        {
            return db.AddSuppliers(s);
        }

        public List<Suppliers> GetSuppliers()
        {
            return db.GetSuppliers();
        }

        public bool UpdateSuppliers(Suppliers s)
        {
            return db.UpdateSuppliers(s);
        }

        public bool AddPaymentVoucher(PaymentVoucher p)
        {
            return db.AddPaymentVoucher(p);
        }

        public List<PaymentVoucher> GetPaymentVouchers()
        {
            return db.GetPaymentVouchers();
        }

        public bool AddInvoice(Invoice i, List<InvoiceDetails> lstInvDetails)
        {
            return db.AddInvoice(i, lstInvDetails);
        }

        public List<Invoice> GetInvoices()
        {
            return db.GetInvoices();
        }

        public List<InvoiceDetails> GetInvoiceDetails(string QuotationNo)
        {
            return db.GetInvoiceDetails(QuotationNo);
        }

        public bool UpdateInvoice(Invoice i)
        {
            return db.UpdateInvoice(i);
        }

        public List<GetReconReport> GetReconReport(string SupplierID)
        {
            return db.GetReconReport(SupplierID);
        }
    }
}
