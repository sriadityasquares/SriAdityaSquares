using BusinessLayer;
using log4net;
using ModelLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        BookingBL booking = new BookingBL();
        CommonBL common = new CommonBL();
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DayWiseReport()
        {
            
            return View();
        }

        public ActionResult PeriodWiseBookingReport()
        {

            List<Projects> projectList = booking.BindProjects();
            Projects p = new Projects();
            p.ProjectID = 9999;
            p.ProjectName = "All";
            projectList.Add(p);
            ViewBag.ProjectList = new SelectList(projectList.OrderByDescending(x => x.ProjectID), "ProjectID", "ProjectName");

            List<Year> years = common.BindYear();
            List<Month> months = common.BindMonth();
            ViewBag.YearList = new SelectList(years, "YearName", "YearName");
            ViewBag.MonthList = new SelectList(months, "ID", "MonthName");
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetPeriodWiseBookingReport(int option,string fromDate,string toDate,string projects,string years,string months) 
        {
            if(projects.Contains("9999"))
            {
                foreach(var project in booking.BindProjects())
                {
                    projects = projects  +","+project.ProjectID.ToString(); 
                }
            }
            ReportBL rep = new ReportBL();
            List<GetPeriodWiseBookingDetails> list = rep.BindPeriodWiseBookingInfo(option, fromDate, toDate, projects, years, months);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetGraphicalPeriodWiseBookingReport(int option, int projects, string years, string months)
        {
            ReportBL rep = new ReportBL();
            List<GetGraphicalPeriodWiseBooking> list = rep.BindGraphicalPeriodWiseBookingInfo(option, projects, years, months);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult BookingInfoReport()
        {
            List<Projects> projectList = booking.BindProjects();
            Projects p = new Projects();
            p.ProjectID = 9999;
            p.ProjectName = "All";
            projectList.Add(p);
            ViewBag.ProjectList = new SelectList(projectList.OrderByDescending(x => x.ProjectID), "ProjectID", "ProjectName");

            ViewBag.ProjectList = new SelectList(projectList.OrderByDescending(x => x.ProjectID), "ProjectID", "ProjectName");
            return View();
        }

        [Authorize(Roles = "Admin,Client")]
        public ActionResult PaymentInfoReport()
        {
            //ViewBag.CanDisplay = false;
            List<Projects> projectList = new List<Projects>();
            if (User.IsInRole("Client"))
            {
                projectList = booking.BindClientProjects(User.Identity.Name);
                ViewBag.CanDisplay = false;
            }
            else
            {
                projectList = booking.BindProjects();
                ViewBag.CanDisplay = true;
            }
                Projects p = new Projects();
                p.ProjectID = 9999;
                p.ProjectName = "All";
                projectList.Add(p);
            
            ViewBag.ProjectList = new SelectList(projectList.OrderByDescending(x => x.ProjectID), "ProjectID", "ProjectName");
            return View();
        }

        public JsonResult GetBookingReportbyDate(string start, string end,string projectID)
        {
            ReportBL rep = new ReportBL();
            if (projectID.Contains("9999"))
            {
                foreach (var project in booking.BindProjects())
                {
                    projectID = projectID + "," + project.ProjectID.ToString();
                }
            }
            List<GetBookingInfoByDate> list = rep.BindBookingInfo(start, end, projectID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPaymentReportbyDate(string start, string end, string projectID,int type)
        {
            ReportBL rep = new ReportBL();
            if (projectID.Contains("9999"))
            {
                if (!User.IsInRole("Client"))
                {
                    foreach (var project in booking.BindProjects())
                    {
                        projectID = projectID + "," + project.ProjectID.ToString();
                    }
                    ViewBag.CanDisplay = false;
                }
                else
                {
                    foreach (var project in booking.BindClientProjects(User.Identity.Name))
                    {
                        projectID = projectID + "," + project.ProjectID.ToString();
                    }
                    ViewBag.CanDisplay = true;
                }

            }
            List<GetPaymentInfoByDate> list = rep.BindPaymentInfo(start, end, projectID, type);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AgentWiseBookingReport()
        {
            List<Projects> projectList = booking.BindProjects();
            Projects p = new Projects();
            p.ProjectID = 9999;
            p.ProjectName = "All";
            projectList.Add(p);
            ViewBag.ProjectList = new SelectList(projectList.OrderByDescending(x=>x.ProjectID), "ProjectID", "ProjectName");
            return View();
        }

        
        public JsonResult GetAgentBookingReportbyDate(string start, string end, string projectID)
        {
            ReportBL rep = new ReportBL();
            if (projectID.Contains("9999"))
            {
                foreach (var project in booking.BindProjects())
                {
                    projectID = projectID + "," + project.ProjectID.ToString();
                }
            }
            List<GetAgentWiseBookingDetails> list = rep.BindAgentBookingInfo(start, end, projectID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult BhkWiseBookingReport()
        {
            List<Projects> projectList = booking.BindProjects();
            Projects p = new Projects();
            p.ProjectID = 9999;
            p.ProjectName = "All";
            projectList.Add(p);
            ViewBag.ProjectList = new SelectList(projectList.OrderByDescending(x => x.ProjectID), "ProjectID", "ProjectName");
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult FacingWiseBookingReport()
        {
            List<Projects> projectList = booking.BindProjects();
            Projects p = new Projects();
            p.ProjectID = 9999;
            p.ProjectName = "All";
            projectList.Add(p);
            ViewBag.ProjectList = new SelectList(projectList.OrderByDescending(x => x.ProjectID), "ProjectID", "ProjectName");
            return View();
        }

        public JsonResult GetBhkBookingReportbyDate(string start, string end, string projectID)
        {
            ReportBL rep = new ReportBL();
            if (projectID.Contains("9999"))
            {
                foreach (var project in booking.BindProjects())
                {
                    projectID = projectID + "," + project.ProjectID.ToString();
                }
            }
            List<GetBhkWiseBookingDetails> list = rep.BindBhkBookingInfo(start, end, projectID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFacingBookingReportbyDate(string start, string end, string projectID)
        {
            ReportBL rep = new ReportBL();
            if (projectID.Contains("9999"))
            {
                foreach (var project in booking.BindProjects())
                {
                    projectID = projectID + "," + project.ProjectID.ToString();
                }
            }
            List<GetFacingWiseBookingDetails> list = rep.BindFacingBookingInfo(start, end, projectID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult FlatWiseAgentCommission()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            return View();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult FlatPaymentDetails()
        {
            
            return View();
        }

        public JsonResult GetFlatPaymemts()
        {
            ReportBL rep = new ReportBL();
            var list = rep.GetFlatPayments();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFlatAgentCommission(int projectID, int towerID)
        {
            ReportBL rep = new ReportBL();
            List<GetFlatWiseTotalAgentCommission> list = rep.BindFlatAgentCommission(projectID,towerID);
            
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFlatAgentCommissionDetails()
        {
            ReportBL rep = new ReportBL();
            //var flatAgent = JsonConvert.DeserializeObject<List<GetFlatWiseTotalAgentCommission>>(data);
            //GetFlatWiseTotalAgentCommission datalist = JsonConvert.DeserializeObject<GetFlatWiseTotalAgentCommission>(data);
            List<FlatWiseAgentCommission> list =  rep.BindFlatAgentCommissionDetails();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GraphicalPeriodWiseBookingReport()
        {
            List<Projects> projectList = booking.BindProjects();
            List<Year> years = common.BindYear();
            List<Month> months = common.BindMonth();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            //ViewBag.ProjectList = new SelectList(projectList, "ProjectID", "ProjectName");
            ViewBag.YearList = new SelectList(years, "YearName", "YearName");
            ViewBag.MonthList = new SelectList(months, "ID", "MonthName");
            return View();
        }

        public ActionResult CustomerEnquiry()
        {
            return View();
        }

        public JsonResult GetCustomerEnquiries()
        {
            ReportBL rep = new ReportBL();
            //var flatAgent = JsonConvert.DeserializeObject<List<GetFlatWiseTotalAgentCommission>>(data);
            //GetFlatWiseTotalAgentCommission datalist = JsonConvert.DeserializeObject<GetFlatWiseTotalAgentCommission>(data);
            List<CustomerEnquiry> list = rep.GetCustomerEnquiries();
            foreach(var item in list)
            {
                item.Recipients = item.Recipients.Replace(",", "<br>");
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EligibleFlatsForCommission()
        {
            return View();
        }

        public JsonResult GetEligibleFlatsForCommission()
        {
            ReportBL rep = new ReportBL();
            var lstEligibleBookings = rep.GetEligibleFlatsForCommission();
            return Json(lstEligibleBookings, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SchemeBasedBookings()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            return View();
        }

        public JsonResult GetSchemeBasedBookings(int towerID)
        {
            var list = booking.BindSchemeBasedBookings(towerID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AgentPercentages()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            return View();
        }

        public ActionResult GetAgentPercentages(int projectID)
        {
            var agentPercentage = booking.GetAgentPercentagesByProject(projectID);
            return Json(agentPercentage, JsonRequestBehavior.AllowGet);
            
        }


        [Authorize(Roles = "Admin")]
        public ActionResult SMSLog()
        {
            return View();
        }

        public JsonResult GetSMSLogs()
        {
            var sms = common.GetSMSLogs();
            return Json(sms, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetCancelledFlatInformation()
        {
            return View();
        }

        public JsonResult GetCancelledFlatInfo()
        {
            var sms = booking.GetCancelledFlatsInfo();
            return Json(sms, JsonRequestBehavior.AllowGet);
        }
    }
}