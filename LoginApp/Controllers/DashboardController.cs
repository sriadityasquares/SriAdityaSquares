using BusinessLayer;
using Microsoft.AspNet.Identity;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LoginApp.Controllers
{
    public class DashboardController : Controller
    {
        BookingBL booking = new BookingBL();
        // GET: Dashboard
        public ActionResult Index()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            return View();
        }

        public ActionResult Agent()
        {
            return View();
        }

        public ActionResult Agents()
        {
            return View();
        }

        public JsonResult GetAgentCommissionByAgentLogin()
        {
            var email = User.Identity.Name;
            List<FlatWiseAgentCommission> list = booking.BindAgentDashboard(email);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAgentCommissionByAgentLogins()
        {
            var email = User.Identity.Name;
            List<FlatWiseAgentCommission> list = booking.BindAgentsDashboard(email);
            //JavaScriptSerializer jss = new JavaScriptSerializer();

            //string output = jss.Serialize(list);
            list[0].AgentSponserCode = null;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}