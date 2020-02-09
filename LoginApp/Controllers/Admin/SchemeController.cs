using BusinessLayer;
using log4net;
using ModelLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers.Admin
{
    public class SchemeController : Controller
    {
        private static readonly ILog log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<Schemes> schemeList = new List<Schemes>();
        BookingBL booking = new BookingBL();
        AdminBL schemes = new AdminBL();
        // GET: Scheme
        public ActionResult Index()
        {
            return View();
        }

        // GET: Scheme/Details/5
        public ActionResult Details()
        {
            schemeList = booking.BindSchemeDetails();
            return Json(schemeList, JsonRequestBehavior.AllowGet);
        }

        // GET: Scheme/Create
        [HttpGet]
        public ActionResult Create(string models)
        {
            bool result = false;
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<Schemes> data = JsonConvert.DeserializeObject<List<Schemes>>(models,settings);
                data[0].CreatedBy = User.Identity.Name;
                data[0].CreatedDate = System.DateTime.Now;
                result = schemes.AddScheme(data[0]);


                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        // GET: Scheme/Edit/5
        [HttpGet]
        public ActionResult Update(string models)
        {
            bool result = false;
            try
            {
                List<Schemes> data = JsonConvert.DeserializeObject<List<Schemes>>(models);
                data[0].UpdatedBy = User.Identity.Name;
                data[0].UpdatedDate = System.DateTime.Now;
                result = schemes.UpdateScheme(data[0]);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Scheme/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Scheme/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Scheme/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
