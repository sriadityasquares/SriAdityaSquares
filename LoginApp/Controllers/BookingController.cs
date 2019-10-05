using DataLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class BookingController : Controller
    {
        Common objCasc = new Common();
        // GET: Booking
        public ActionResult Index()
        {
            List<tblCountry> countryList = objCasc.BindCountry();
            List<Projects> projectList = objCasc.BindProjects();
            ViewBag.CountryList = new SelectList(countryList, "CountryID", "CountryName");
            ViewBag.ProjectList = new SelectList(projectList, "ProjectID", "ProjectName");
            ViewBag.AgentList = new SelectList(countryList, "CountryID", "CountryName");
            return View();
        }
    }
}