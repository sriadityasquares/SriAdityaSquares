using BusinessLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace LoginApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        BookingBL booking = new BookingBL();
        // GET: Dashboard
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public DashboardController()
        {

        }

        public DashboardController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public ActionResult Index()
        {
            List<Projects> projectList = new List<Projects>();
            if (User.IsInRole("Client"))
            {
                projectList = booking.BindClientProjects(User.Identity.Name);
            }
            else
                projectList = booking.BindProjects();
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

        public ActionResult FlatLifeCycle()
        {
            List<Projects> projectList = new List<Projects>();
            if (User.IsInRole("Client"))
            {
                projectList = booking.BindClientProjects(User.Identity.Name);
            }
            else
                projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            return View();
        }

        public ActionResult GetFlatLifeCycle(int flatID)
        {
            var flatLifeCycle = booking.BindFlatLifeCycle(flatID);
            return Json(flatLifeCycle, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAgentCommissionByAgentLogin()
        {
            var email = User.Identity.Name;
            List<FlatWiseAgentCommission> list = booking.BindAgentDashboard(email);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FlatWiseAgentCommission()
        {
            return View();
        }

        public JsonResult GetAgentCommissionByAgentLogins()
        {
            var email = "";
            var _user = UserManager.FindByEmail(User.Identity.Name);
            if (UserManager.IsInRole(_user.Id, "Admin"))
            {
                email = "nsrinivas78@gmail.com";
            }
            else
            {
                email = User.Identity.Name;
            }
            List<FlatWiseAgentCommission> list = booking.BindAgentsDashboard(email);

            if (list.Count > 0) list[0].AgentSponserCode = null;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAgentFlatWiseCommissionByAgentLogins()
        {
            var email = "";
            var _user = UserManager.FindByEmail(User.Identity.Name);
            if (UserManager.IsInRole(_user.Id, "Admin"))
            {
                email = "nsrinivas78@gmail.com";
            }
            else
            {
                email = User.Identity.Name;
            }
            List<GetAgentFlatWiseCommissionByLogin> list = booking.BindFlatWiseAgentsCommissionByLogins(email);

            //if (list.Count > 0) list[0].AgentSponserCode = null;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BookingStatistics()
        {
            List<Projects> projectList = new List<Projects>();
            if (User.IsInRole("Client"))
            {
                projectList = booking.BindClientProjects(User.Identity.Name);
            }
            else
                projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            return View();
        }

        public JsonResult GetBookingStatistics(int TowerId)
        {
            var list = booking.BindBookingStatistics(TowerId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}