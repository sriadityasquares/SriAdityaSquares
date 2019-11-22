using BusinessLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}