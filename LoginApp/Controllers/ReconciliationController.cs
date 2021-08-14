using BusinessLayer;
using Microsoft.AspNet.Identity.Owin;
using ModelLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class ReconciliationController : Controller
    {
        // GET: Reconciliation
        ReconBL recon = new ReconBL();
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
    }
}