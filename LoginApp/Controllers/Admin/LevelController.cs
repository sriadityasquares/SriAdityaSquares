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
    public class LevelController : Controller
    {
        private static readonly ILog log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<LevelsMaster> levelList = new List<LevelsMaster>();
        BookingBL booking = new BookingBL();
        AdminBL project = new AdminBL();
        // GET: Level
        public ActionResult Index()
        {
            return View();
        }

        // GET: Level/Details/5
        public ActionResult Details()
        {
            try
            {
                levelList = booking.BindLevelsMasters();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(levelList, JsonRequestBehavior.AllowGet);
        }

        // GET: Level/Create
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
                List<LevelsMaster> data = JsonConvert.DeserializeObject<List<LevelsMaster>>(models, settings);
                data[0].CreatedBy = User.Identity.Name;
                data[0].CreatedDate = System.DateTime.Now;
                result = project.AddLevel(data[0]);


                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Update(string models)
        {
            bool result = false;
            try
            {
                List<LevelsMaster> data = JsonConvert.DeserializeObject<List<LevelsMaster>>(models);
                data[0].UpdatedBy = User.Identity.Name;
                data[0].UpdatedDate = System.DateTime.Now;
                result = project.UpdateLevel(data[0]);


                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        
        
    }
}
