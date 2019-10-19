using BusinessLayer;
using DataLayer;
using LoginApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ModelLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class BookingController : Controller
    {
        CommonBL common = new CommonBL();
        BookingBL booking = new BookingBL();
        public static List<Country> countryList;
        public static List<Projects> projectList;
        private ApplicationUserManager _userManager;
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
        // GET: Booking
        public ActionResult Index()
        {
            countryList = common.BindCountry();
            projectList = booking.BindProjects();
            var json = JsonConvert.SerializeObject(projectList);
            ViewBag.CountryList = new SelectList(countryList, "CountryID", "CountryName");
            ViewBag.ProjectList = new SelectList(projectList, "ProjectID", "ProjectName");
            //ViewBag.AgentList = new SelectList(countryList, "CountryID", "CountryName");
            return View();
        }

        public ActionResult New()
        {
            countryList = common.BindCountry();
            projectList = booking.BindProjects();
            ViewBag.CountryList = new SelectList(countryList, "CountryID", "CountryName");
            ViewBag.ProjectList = new SelectList(projectList, "ProjectID", "ProjectName");
            return View();
        }

        

        [HttpPost]
        public ActionResult New(ModelLayer.BookingInformation b)
        {
            bool result = booking.SaveNewBooking(b);
            if(result)
            {
                //ViewBag.result = "Booking Successfull";
                TempData["successmessage"] = "Booking Successfull";
                var user = new ApplicationUser { UserName = b.Email, Email = b.Email };

                UserManager.CreateAsync(user, "Welcome@123");
                UserManager.AddToRoleAsync(user.Id, "Customer");
            }
            else
            {
                TempData["successmessage"] = "Booking Failed";
            }
            ModelState.Clear();
            countryList = common.BindCountry();
            projectList = booking.BindProjects();
            
            ViewBag.CountryList = new SelectList(countryList, "CountryID", "CountryName");
            ViewBag.ProjectList = new SelectList(projectList, "ProjectID", "ProjectName");
            //ViewBag.AgentList = new SelectList(countryList, "CountryID", "CountryName");
            return View();
        }


        public JsonResult GetTowers(int ProjectId)
        {
            List<Towers> CityList = booking.BindTowers(ProjectId);
            return Json(CityList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetFlats(int TowerId)
        {
            List<Flats> CityList = booking.BindFlats(TowerId);
            return Json(CityList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetProjectAgents(int ProjectId)
        {
            List<AgentProjectLevel> AgentList = booking.BindProjectAgents(ProjectId);
            return Json(AgentList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetFlatDetails(int FlatId,int ProjectID)
        {
            List<FlatDetails> FlatDetails = booking.BindFlatDetails(FlatId, ProjectID);
            return Json(FlatDetails, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetProjects()
        {
            projectList = booking.BindProjects();
            return Json(projectList, JsonRequestBehavior.AllowGet);

        }
    }
}