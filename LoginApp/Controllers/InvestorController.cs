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
    public class InvestorController : Controller
    {
        // GET: Investor
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
            var landlordData = admin.BindInvestors();
            return Json(landlordData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateInvestor(string models)
        {
            bool result = false;
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<Investor> data = JsonConvert.DeserializeObject<List<Investor>>(models, settings);


                data[0].CreatedDate = DateTime.Now;
                data[0].CreatedBy = User.Identity.Name;
                result = admin.AddInvestor(data[0]);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateInvestor(string models)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<Investor> data = JsonConvert.DeserializeObject<List<Investor>>(models, settings);
                data[0].UpdatedBy = User.Identity.Name;
                data[0].UpdatedDate = DateTime.Now;
                var result = admin.UpdateInvestor(data[0]);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


    }
}