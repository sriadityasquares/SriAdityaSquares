using BusinessLayer;
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
            flatList = booking.BindAllFlats();
            return Json(flatList, JsonRequestBehavior.AllowGet);
        }

        // GET: Flat/Create
        [HttpGet]
        public ActionResult CreateFlat(string models)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            List<Flats> data = JsonConvert.DeserializeObject<List<Flats>>(models, settings);
            var result = flat.AddFlat(data[0]);
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult UpdateFlat(string models)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            List<Flats> data = JsonConvert.DeserializeObject<List<Flats>>(models, settings);
            var result = flat.UpdateFlat(data[0]);
            //var result = towers.UpdateTower(data[0]);
            return Json(true, JsonRequestBehavior.AllowGet);
        }


    }
}
