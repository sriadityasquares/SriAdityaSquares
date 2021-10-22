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
    public class ProjectExpenseTypeController : Controller
    {
        private static readonly ILog log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<ProjectExpenseCategory> projectexpList = new List<ProjectExpenseCategory>();
        BookingBL booking = new BookingBL();
        AdminBL project = new AdminBL();
        // GET: ProjectExpenseType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            try
            {
                projectexpList = booking.BindProjectExpenseCategory();
               
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(projectexpList, JsonRequestBehavior.AllowGet);
        }
        // POST: Project/Create
        [HttpGet]
        public ActionResult Create(string models)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<ProjectExpenseCategory> data = JsonConvert.DeserializeObject<List<ProjectExpenseCategory>>(models, settings);
                data[0].CreatedBy = User.Identity.Name;
                var result = project.AddProjectExpenseCategory(data[0]);
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
        public ActionResult Update(string models)
        {
            try
            {
                List<ProjectExpenseCategory> data = JsonConvert.DeserializeObject<List<ProjectExpenseCategory>>(models);
                data[0].CreatedBy = User.Identity.Name;
                var result = project.UpdateProjectExpenseCategory(data[0]);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}