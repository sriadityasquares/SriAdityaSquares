using BusinessLayer;
using Microsoft.AspNet.Identity.Owin;
using ModelLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class ReconciliationController : Controller
    {
        // GET: Reconciliation
        ReconBL recon = new ReconBL();
        BookingBL booking = new BookingBL();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Suppliers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Suppliers(Suppliers s)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var stringChars = new char[3];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            s.SupplierID = new String(stringChars) + s.SupplierPhone.Substring(s.SupplierPhone.Length - 4, 4);
            s.CreatedBy = User.Identity.Name;
            s.CreatedDate = DateTime.Now;
            var result = recon.AddSuppliers(s);
            if (result)
            {
                TempData["successmessage"] = "Supplier Added Successfully";

            }
            else
            {
                TempData["successmessage"] = "Supplier Adding Failed";
            }
            ModelState.Clear();
            return View();
        }

        public JsonResult GetSuppliers()
        {
            var result = recon.GetSuppliers();
            TempData["Suppliers"] = result;
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult UpdateSuppliers(string models)
        {

            List<Suppliers> data = JsonConvert.DeserializeObject<List<Suppliers>>(models);
            var result = recon.UpdateSuppliers(data[0]);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PaymentVoucher()
        {
            var suppliers = recon.GetSuppliers();
            TempData["SupplierList"] = new SelectList(suppliers, "SupplierID", "SupplierName");
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult PaymentVoucher(PaymentVoucher p)
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var stringChars = new char[7];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            p.VoucherNo = new String(stringChars);
            var suppliers = recon.GetSuppliers();
            p.SupplierName = suppliers.Where(x => x.SupplierID == p.SupplierID).Select(x => x.SupplierName).FirstOrDefault();
            p.SupplierPhone = suppliers.Where(x => x.SupplierID == p.SupplierID).Select(x => x.SupplierPhone).FirstOrDefault();
            //string dateString = null;
            CultureInfo provider = CultureInfo.InvariantCulture;
            // It throws Argument null exception  
            p.PaymentDate = DateTime.ParseExact(p.PaidDate, "dd/MM/yyyy", provider);
            //p.PaymentDate = Convert.ToDateTime(p.PaidDate);
            p.CreatedBy = User.Identity.Name;
            p.CreatedDate = DateTime.Now;
            var result = recon.AddPaymentVoucher(p);
            if (result)
            {
                TempData["successmessage"] = "Payment Voucher Added Successfully";

            }
            else
            {
                TempData["successmessage"] = "Payment Voucher Adding Failed";
            }
            ModelState.Clear();

            TempData["SupplierList"] = new SelectList(suppliers, "SupplierID", "SupplierName");
            TempData.Keep();
            return View();
        }

        public JsonResult GetPaymentVoucher()
        {
            var result = recon.GetPaymentVouchers();
            foreach (var item in result)
            {
                item.PaidDate = Convert.ToDateTime(item.PaymentDate).ToString("dd/MM/yyyy");
                //item.PrintVoucher = "<a class ='btn' onClick=GetReceipt('" + item.VoucherNo + "')>"+"Print Voucher"+"<i class='fa fa-file-pdf-o'></i></a >";
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Invoices()
        {
            var suppliers = recon.GetSuppliers();
            List<Projects> projectList = new List<Projects>();
            projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep();
            TempData["SupplierList"] = new SelectList(suppliers, "SupplierID", "SupplierName");
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult Invoices(Invoice invoice)
        {
            var suppliers = recon.GetSuppliers();
            List<Projects> projectList = new List<Projects>();
            projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep();
            TempData["SupplierList"] = new SelectList(suppliers, "SupplierID", "SupplierName");
            TempData.Keep();
            if (ModelState.IsValid)
            {
                
                List<InvoiceDetails> lstInvoiceDetails = JsonConvert.DeserializeObject<List<InvoiceDetails>>(invoice.InvoiceDetails);
                foreach (var item in lstInvoiceDetails)
                {
                    item.InvoiceNo = invoice.InvoiceNo;
                    item.QuotationNo = invoice.QuotationNo;
                    item.ID = Guid.NewGuid();
                    item.CreatedBy = User.Identity.Name;
                    item.CreatedDate = DateTime.Now;
                }
                CultureInfo provider = CultureInfo.InvariantCulture;
                // It throws Argument null exception  
                invoice.InvoiceDate = DateTime.ParseExact(invoice.InvDate, "dd/MM/yyyy", provider);
                invoice.SupplierName = suppliers.Where(x => x.SupplierID == invoice.SupplierID).Select(x => x.SupplierName).FirstOrDefault();
                invoice.SupplierPhone = suppliers.Where(x => x.SupplierID == invoice.SupplierID).Select(x => x.SupplierPhone).FirstOrDefault();
                invoice.CreatedBy = User.Identity.Name;
                invoice.CreatedDate = DateTime.Now;
                var result = recon.AddInvoice(invoice, lstInvoiceDetails);
                ModelState.Clear();
                if (result)
                {
                    TempData["successmessage"] = "Invoice Added Successfully";

                }
                else
                {
                    TempData["successmessage"] = "Invoice Adding Failed";
                }

                
            }
            return View();
        }


        public JsonResult GetInvoices()
        {
            var result = recon.GetInvoices();
            foreach (var item in result)
            {
                item.InvDate = Convert.ToDateTime(item.InvoiceDate).ToString("dd/MM/yyyy");
                if(item.ProjectID == null)
                {
                    item.ProjectName = "NA";
                }
                if(item.InvoiceNo == null)
                {
                    item.InvoiceNo = "";
                }
                //item.PrintVoucher = "<a class ='btn' onClick=GetReceipt('" + item.VoucherNo + "')>"+"Print Voucher"+"<i class='fa fa-file-pdf-o'></i></a >";
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public string GetInvoiceDetails(string QuotationNo)
        {
            var result = recon.GetInvoiceDetails(QuotationNo);
            string tableResult = "<table><tbody><tr><th>SerialNo</th><th>Description</th><th>Unit Price</th><th>Quantity</th><th>Amount</th></tr>";
            foreach (var item in result)
            {
                tableResult = tableResult + "<tr><td>" + item.SerialNo + "</td><td>" + item.Description + "</td><td>" + item.UnitPrice + "</td><td>" + item.Quantity + "</td><td>&#8377;." + item.Amount + "</td></tr>";
            }
            return tableResult;

        }

        public JsonResult UpdateInvoice(string models)
        {
            
            List<Invoice> data = JsonConvert.DeserializeObject<List<Invoice>>(models);
            if (data[0].InvoiceNo != null && data[0].InvoiceNo != "")
            {
                var result = recon.UpdateInvoice(data[0]);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReconSummary()
        {
            var suppliers = recon.GetSuppliers();
            List<Projects> projectList = new List<Projects>();
            projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep();
            TempData["SupplierList"] = new SelectList(suppliers, "SupplierID", "SupplierName");
            
            TempData.Keep();
            return View();
        }

        public JsonResult GetReconSummary(string SupplierID)
        {
            var result = recon.GetReconReport(SupplierID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}