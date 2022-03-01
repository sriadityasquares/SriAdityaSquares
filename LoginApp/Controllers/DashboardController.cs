using BusinessLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
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


        [Authorize(Roles = "Admin,DataEntry,Agent,Manager,Employee")]
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
            var lstProTrans = booking.GetProjectTransactions();
            foreach(var trans in lstProTrans)
            {
                trans.TotalSalesAmount = Helper.AmountInIndianCurrency(trans.TotalSalesAmount);
                trans.CollectedAmount = Helper.AmountInIndianCurrency(trans.CollectedAmount);
                trans.BalanceAmount = Helper.AmountInIndianCurrency(trans.BalanceAmount);
                trans.SchemeWiseDue = Helper.AmountInIndianCurrency(trans.SchemeWiseDue);
            }


            ViewBag.ProjectTransactions = lstProTrans;
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
            if (User.IsInRole("Admin"))
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
                xBookingGraph = xBookingGraph + Convert.ToDateTime(item.xaxis).Day.ToString() + ",";
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


            //var lstDailyExpenses = booking.BindDailyExpenseGraph();
            //ViewBag.lstDailyExpenses = Json(lstDailyExpenses,JsonRequestBehavior.AllowGet).Data;
            //var xDailyExpenseGraph = "[";
            //var yDailyExpenseGraph = "[";
            //foreach (var item in lstDailyExpenses)
            //{
            //    xDailyExpenseGraph = xDailyExpenseGraph + "'"+item.xaxis+"'" + ",";
            //    yDailyExpenseGraph = yDailyExpenseGraph + Convert.ToInt32(item.yaxis)/1000 + ",";
            //}
            //ViewBag.xDailyExpenseGraph = xDailyExpenseGraph.Substring(0, xDailyExpenseGraph.Length - 1) + "]";
            //ViewBag.yDailyExpenseGraph = yDailyExpenseGraph.Substring(0, yDailyExpenseGraph.Length - 1) + "]";

            ViewBag.PastDue = booking.GetPastDue();
            ViewBag.AreaRem = booking.GetProjectWiseArea();
            ViewBag.ProjectCategoryWiseExpenses = booking.GetProjectCategoryWiseExpenses();
            return View();
        }

        

        [Authorize(Roles = "Admin,Client,DataEntry,Agent,Manager,Employee,Franchise Owner")]
        public ActionResult Index()
        {
            List<Projects> projectList = new List<Projects>();
            if (User.IsInRole("Client"))
            {
                projectList = booking.BindClientProjects(User.Identity.Name);
            }
            else
                projectList = booking.BindProjects();
            TempData["UserRole"] = "";
            if (User.IsInRole("Franchise Owner"))
            {
                TempData["UserRole"] = "FO";
            }
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
            ViewBag.News = booking.GetNewsUpdates();
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

        [Authorize(Roles = "Admin,Customer,Client")]
        public JsonResult GetFlatLifeCycle(int flatID)
        {
            var isAuthorised = true;
            ViewBag.News = booking.GetNewsUpdates();
            if (User.IsInRole("Customer"))
            {
                var flatList = booking.BindCustomerFlats(User.Identity.Name);
                var exist = flatList.Where(x => x.FlatID == flatID).FirstOrDefault();
                if (exist == null)
                    isAuthorised = false;
                else
                    isAuthorised = true;
            }
            
            if (isAuthorised)
            {
                var flatLifeCycle = booking.BindFlatLifeCycle(flatID);

                double TotalAmount = Convert.ToDouble(flatLifeCycle[0].BalanceAmount) + Convert.ToDouble(flatLifeCycle[0].BookingAmount);
                double BalanceAmount = Convert.ToDouble(flatLifeCycle[flatLifeCycle.Count - 1].BalanceAmount);
                double TotalPaid = Convert.ToDouble(TotalAmount - BalanceAmount);
                var percentagePaid = Math.Round(TotalPaid / TotalAmount * 100, 2);
                flatLifeCycle[0].PercentageCompleted = percentagePaid;
                
                return Json(flatLifeCycle, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("Not Authroized to view other flat details", JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Agent")]
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

        [Authorize(Roles = "Admin,Agent")]
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

        [Authorize(Roles = "Admin,Agent")]
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

        [Authorize(Roles = "Admin,Agent")]
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

        [Authorize(Roles = "Admin,Client,DataEntry,Agent,Manager,Employee")]
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

        [Authorize(Roles = "Admin,Client,DataEntry,Agent,Manager,Employee")]
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

        [Authorize(Roles = "Admin,Agent")]
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

        public JsonResult GetDailyExpensesChart()
        {
            var lstDailyExpenses = booking.BindDailyExpenseGraph();
            return Json(lstDailyExpenses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLandlordPaymentChart()
        {
            var lstLandlordPayments = booking.BindLandlordPaymentChart();
            return Json(lstLandlordPayments, JsonRequestBehavior.AllowGet);
        }
    }
}