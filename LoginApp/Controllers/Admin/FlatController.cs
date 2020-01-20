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
    public class FlatController : Controller
    {
        private static readonly ILog log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<Flats> flatList = new List<Flats>();
        BookingBL booking = new BookingBL();
        AdminBL flat = new AdminBL();
        // GET: Flat
        public ActionResult Index()
        {
            return View();
        }

        // GET: Flat/Details/5
        public ActionResult Details()
        {
            try
            {
                flatList = booking.BindAllFlats();

            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
               
            }

            return Json(flatList, JsonRequestBehavior.AllowGet);
        }

        // GET: Flat/Create
        [HttpGet]
        public ActionResult CreateFlat(string models)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<Flats> data = JsonConvert.DeserializeObject<List<Flats>>(models, settings);
                var result = flat.AddFlat(data[0]);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);

            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult UpdateFlat(string models)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<Flats> data = JsonConvert.DeserializeObject<List<Flats>>(models, settings);
                var result = flat.UpdateFlat(data[0]);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);

            }
            //var result = towers.UpdateTower(data[0]);
            return Json(true, JsonRequestBehavior.AllowGet);
        }


    }
}
