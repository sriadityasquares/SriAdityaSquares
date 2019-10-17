using BusinessLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class ReportController : Controller
    {
        BookingBL booking = new BookingBL();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DayWiseReport()
        {
            return View();
        }

        public ActionResult BookingInfoReport()
        {
            List<Projects> projectList = booking.BindProjects();
            ViewBag.ProjectList = new SelectList(projectList, "ProjectID", "ProjectName");
            return View();
        }

        public JsonResult GetReportbyDate(DateTime start, DateTime end,int projectID)
        {
            ReportBL rep = new ReportBL();
            List<GetBookingInfoByDate> list = rep.BindBookingInfo(start, end, projectID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}