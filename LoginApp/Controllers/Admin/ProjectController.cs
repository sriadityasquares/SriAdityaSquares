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
    public class ProjectController : Controller
    {
        private static readonly ILog log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<Projects> projectList = new List<Projects>();
        BookingBL booking = new BookingBL();
        AdminBL project = new AdminBL();
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        // GET: Project/Details/5
        public ActionResult Details()
        {
            try
            {
                projectList = booking.BindProjects();
                foreach (var item in projectList)
                {
                    switch (item.BookingStatus)
                    {
                        case "O":
                            item.BookingStatusName = "OPEN";
                            break;
                        case "C":
                            item.BookingStatusName = "CLOSED";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        [HttpGet]
        public ActionResult CreateProject(string models)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<Projects> data = JsonConvert.DeserializeObject<List<Projects>>(models,settings);
                var result = project.AddProject(data[0]);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

       
        // POST: Project/Edit/5
        [HttpGet]
        public ActionResult UpdateProject(string models)
        {
            try
            {
                List<Projects> data = JsonConvert.DeserializeObject<List<Projects>>(models);
                var result = project.UpdateProject(data[0]);
                

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                log.Error("Error :" + ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
