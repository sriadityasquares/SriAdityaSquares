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


        [Authorize(Roles = "Admin,DataEntry,Agent")]
        public ActionResult Home()
        {
            var dashboardparams = booking.BindDashboardParameters();
            ViewBag.Projects = dashboardparams.ProjectCount;
            ViewBag.Bookings = dashboardparams.BookingCount;
            ViewBag.Locations = dashboardparams.LocationCount;
            ViewBag.IBOCount = dashboardparams.IBOCount;
            ViewBag.TodayBooking = dashboardparams.TodayBooking;
            ViewBag.TodayPaymentCount = dashboardparams.TodayPaymentCount;
            ViewBag.TodayCustomerCount = dashboardparams.TodayCustomerCount;
            ViewBag.TodayIBOCount = dashboardparams.TodayIBOCount;
            if (dashboardparams.BookingGrowth >= 0)
            {
                ViewBag.isBookingGrowth = true;
                ViewBag.BookingGrowth = dashboardparams.BookingGrowth;
            }
            else
            {
                ViewBag.isBookingGrowth = false;
                ViewBag.BookingGrowth = dashboardparams.BookingGrowth;
            }
            if (dashboardparams.PaymentGrowth >= 0)
            {
                ViewBag.isPaymentGrowth = true;
                ViewBag.PaymentGrowth = dashboardparams.PaymentGrowth;
            }
            else
            {
                ViewBag.isPaymentGrowth = false;
                ViewBag.PaymentGrowth = dashboardparams.PaymentGrowth;
            }
            if (dashboardparams.CustomerGrowth >= 0)
            {
                ViewBag.isCustomerGrowth = true;
                ViewBag.CustomerGrowth = dashboardparams.CustomerGrowth;
            }
            else
            {
                ViewBag.isCustomerGrowth = false;
                ViewBag.CustomerGrowth = dashboardparams.CustomerGrowth;
            }
            if (dashboardparams.IBOGrowth >= 0)
            {
                ViewBag.isIBOGrowth = true;
                ViewBag.IBOGrowth = dashboardparams.IBOGrowth;
            }
            else
            {
                ViewBag.isIBOGrowth = false;
                ViewBag.IBOGrowth = dashboardparams.IBOGrowth;
            }
            ReportBL report = new ReportBL();
            var dueList = report.GetDueReminders();
            ViewBag.PaymentDuesCount = dueList.Count;
            ViewBag.PaymentDues = dueList;
            bool SAS = false;
            if(User.IsInRole("Admin"))
            {
                ViewBag.RecentPayments = booking.BindRecentPayments();
                ViewBag.RecentExpenses = booking.BindRecentExpenses();
                SAS = true;
            }
            ViewBag.TopIBO = booking.BindTopIBO(SAS);
            ViewBag.RecentBooking = booking.BindRecentBooking();
            ViewBag.RecentAgents = booking.BindRecentAgents();
            var lstBookingGraph = booking.BindRecentBookingGraph();
            var xBookingGraph = "[";
            var yBookingGraph = "[";
            foreach (var item in lstBookingGraph)
            {
                xBookingGraph = xBookingGraph + Convert.ToDateTime(item.xaxis).Day.ToString()+",";
                yBookingGraph = yBookingGraph + item.yaxis + ",";
            }
            ViewBag.xBookingGraph = xBookingGraph.Substring(0, xBookingGraph.Length - 1) + "]";
            ViewBag.yBookingGraph = yBookingGraph.Substring(0, yBookingGraph.Length - 1) + "]";

            var lstPaymentGraph = booking.BindRecentPaymentGraph();
            var xPaymentGraph = "[";
            var yPaymentGraph = "[";
            foreach (var item in lstPaymentGraph)
            {
                xPaymentGraph = xPaymentGraph + Convert.ToDateTime(item.xaxis).Day.ToString() + ",";
                yPaymentGraph = yPaymentGraph + item.yaxis + ",";
            }
            ViewBag.xPaymentGraph = xPaymentGraph.Substring(0, xPaymentGraph.Length - 1) + "]";
            ViewBag.yPaymentGraph = yPaymentGraph.Substring(0, yPaymentGraph.Length - 1) + "]";


            var lstIBOGraph = booking.BindRecentAddedIBOGraph();
            var xIBOGraph = "[";
            var yIBOGraph = "[";
            foreach (var item in lstIBOGraph)
            {
                xIBOGraph = xIBOGraph + Convert.ToDateTime(item.xaxis).Day.ToString() + ",";
                yIBOGraph = yIBOGraph + item.yaxis + ",";
            }
            ViewBag.xIBOGraph = xIBOGraph.Substring(0, xIBOGraph.Length - 1) + "]";
            ViewBag.yIBOGraph = yIBOGraph.Substring(0, yIBOGraph.Length - 1) + "]";
            return View();
        }



        [Authorize(Roles = "Admin,Client,DataEntry,Agent")]
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

        [Authorize(Roles = "Admin,Agent")]
        public ActionResult Agents()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Customer,Client")]
        public ActionResult FlatLifeCycle()
        {
            List<Projects> projectList = new List<Projects>();
            if (User.IsInRole("Client"))
            {
                projectList = booking.BindClientProjects(User.Identity.Name);
            }
            else if (User.IsInRole("Customer"))
            {
                projectList = booking.BindCustomerProjects(User.Identity.Name);
            }
            else
                projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            return View();
        }

        public ActionResult GetFlatLifeCycle(int flatID)
        {
            var flatLifeCycle = booking.BindFlatLifeCycle(flatID);
            double TotalAmount = Convert.ToDouble(flatLifeCycle[0].BalanceAmount) + Convert.ToDouble(flatLifeCycle[0].BookingAmount);
            double BalanceAmount = Convert.ToDouble(flatLifeCycle[flatLifeCycle.Count - 1].BalanceAmount);
            double TotalPaid = Convert.ToDouble(TotalAmount - BalanceAmount);
            var percentagePaid = Math.Round(TotalPaid / TotalAmount * 100, 2);
            flatLifeCycle[0].PercentageCompleted = percentagePaid;
            return Json(flatLifeCycle, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAgentCommissionByAgentLogin()
        {
            var email = User.Identity.Name;
            List<FlatWiseAgentCommission> list = booking.BindAgentDashboard(email);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Agent")]
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


        [Authorize(Roles = "Admin,Agent")]
        public ActionResult AgentGraphicalHierarchy()
        {
            return View();
        }


        public JsonResult GetAgentGraphicalHierarchy()
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
            List<TreeObject> list = booking.GetAgentGraphicalHierarchy(email);

            //if (list.Count > 0) list[0].AgentSponserCode = null;
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

        [Authorize(Roles = "Admin,Client,DataEntry,Agent")]
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

        [Authorize(Roles = "Admin,Agent")]
        public ActionResult AgentBookingStats()
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

        public JsonResult GetAgentBookingStats(string projectID, string fromDate, string toDate)
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
            var list = booking.GetAgentBookingGraph(email, projectID, fromDate, toDate);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}