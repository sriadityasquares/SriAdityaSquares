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
    [Authorize(Roles = "Admin")]
    public class TowerController : Controller
    {
        private static readonly ILog log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<GetTowerDetails> towerList = new List<GetTowerDetails>();
        BookingBL booking = new BookingBL();
        AdminBL towers = new AdminBL();
        // GET: Tower
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tower/Details/5
        public ActionResult Details()
        {
            towerList = booking.BindTowerDetails();
            return Json(towerList, JsonRequestBehavior.AllowGet);
        }

        // GET: Tower/Create
        [HttpGet]
        public ActionResult CreateTower(string models)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<Towers> data = JsonConvert.DeserializeObject<List<Towers>>(models, settings);
                data[0].ProjectID = Convert.ToInt32(data[0].ProjectName);
                data[0].CreatedBy = User.Identity.Name;
                var result = towers.AddTower(data[0]);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult UpdateTower(string models)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<Towers> data = JsonConvert.DeserializeObject<List<Towers>>(models, settings);
                data[0].UpdatedBy = User.Identity.Name;
                var result = towers.UpdateTower(data[0]);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}
