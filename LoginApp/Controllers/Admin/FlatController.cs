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
    [Authorize]
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
            bool result = false;
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<Flats> data = JsonConvert.DeserializeObject<List<Flats>>(models, settings);
                data[0].CreatedBy = User.Identity.Name;
                data[0].CreatedDate = DateTime.Now.Date;
                result = flat.AddFlat(data[0]);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult UpdateFlat(string models)
        {
            bool result = false;
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<Flats> data = JsonConvert.DeserializeObject<List<Flats>>(models, settings);
                data[0].UpdatedBy = User.Identity.Name;
                data[0].UpdatedDate = DateTime.Now.Date;
                result = flat.UpdateFlat(data[0]);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);

            }
            //var result = towers.UpdateTower(data[0]);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}
