using BusinessLayer;
using log4net;
using ModelLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LandLordController : Controller
    {
        // GET: LandLord
        AdminBL admin = new AdminBL();
        BookingBL booking = new BookingBL();
        private static readonly ILog log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            var landlordData = admin.BindLandlords();
            return Json(landlordData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateLandlord(string models)
        {
            bool result = false;
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<LandLord> data = JsonConvert.DeserializeObject<List<LandLord>>(models, settings);

                
                data[0].CreatedDate = DateTime.Now;
                data[0].CreatedBy = User.Identity.Name;
                result = admin.AddLandLord(data[0]);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateLandlord(string models)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<LandLord> data = JsonConvert.DeserializeObject<List<LandLord>>(models, settings);
                data[0].UpdatedBy = User.Identity.Name;
                data[0].UpdatedDate = DateTime.Now;
                var result = admin.UpdateLandLord(data[0]);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Payments()
        {
            var landlordDetails = admin.BindLandlords();
            TempData["Landlord"] = new SelectList(landlordDetails, "ID", "Name");
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult Payments(LandlordPayments lp)
        {
            //lp.PaymentDate = Convert.ToDateTime(lp.PayDate);
            lp.PaymentDate = DateTime.ParseExact(lp.PayDate, "dd/MM/yyyy", null);
            lp.CreatedDate = DateTime.Now;
            lp.CreatedBy = User.Identity.Name;
            var result = admin.AddLandLordPayments(lp);
            if (result)
            {
                TempData["successmessage"] = "Payment Added Successfully";

            }
            else
            {
                TempData["successmessage"] = "Payment Adding Failed";
            }
            TempData.Keep();
            ModelState.Clear();
            return View();
        }

        public ActionResult GetPayments()
        {
            var landlordData = admin.GetPayments();
            return Json(landlordData, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult UpdateLandlordPayments(string models)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<LandlordPayments> data = JsonConvert.DeserializeObject<List<LandlordPayments>>(models, settings);
                
                var result = admin.UpdateLandLordPayments(data[0]);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}