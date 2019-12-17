using BusinessLayer;
using DataLayer;
using LoginApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ModelLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class BookingController : Controller
    {
        CommonBL common = new CommonBL();
        BookingBL booking = new BookingBL();

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
            List<Country> countryList = common.BindCountry();
            List<Projects> projectList = booking.BindProjects();
            var json = JsonConvert.SerializeObject(projectList);
            ViewBag.CountryList = new SelectList(countryList, "CountryID", "CountryName");
            ViewBag.ProjectList = new SelectList(projectList, "ProjectID", "ProjectName");
            //ViewBag.AgentList = new SelectList(countryList, "CountryID", "CountryName");
            return View();
        }

        public ActionResult New(int ProjectID = 0,int TowerID = 0,int FlatID = 0)
        {
            List<Country> countryList = common.BindCountry();
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectID"] = ProjectID;
            TempData["TowerID"] = TowerID;
            TempData["FlatID"] = FlatID;
            TempData["CountryList"] = new SelectList(countryList, "CountryID", "CountryName");
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            TempData.Keep("CountryList");
            return View();
        }



        [HttpPost]
        public ActionResult New(ModelLayer.BookingInformation b)
        {
            bool result = booking.SaveNewBooking(b);
            if (result)
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
            TempData.Keep("ProjectList");
            TempData.Keep("CountryList");
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

        public JsonResult GetAllFlats(int TowerId)
        {
            List<Flats> flatList = booking.BindAllFlats(TowerId);
            //var dir = HttpContext.Current.Request.Url.PathAndQuery;
            //var dir = Server.MapPath("/Content/Images");
            //foreach (var flat in flatList)
            //{

            //    if(flat.FlatPlanURL != null)
            //    {
            //        var path = Path.Combine(dir,flat.FlatPlanURL);
            //        flat.FlatPlanURL = path.Replace(@"\\", @"\");
            //    }
            //}
            return Json(flatList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetProjectAgents(int ProjectId)
        {
            List<AgentProjectLevel> AgentList = booking.BindProjectAgents(ProjectId);
            return Json(AgentList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetSchemes(int ProjectId)
        {
            List<Schemes> SchemeList = booking.BindSchemes(ProjectId);
            return Json(SchemeList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetFlatDetails(int FlatId, int ProjectID)
        {
            List<FlatDetails> FlatDetails = booking.BindFlatDetails(FlatId, ProjectID);
            return Json(FlatDetails, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetProjects()
        {
            List<Projects> projectList = booking.BindProjects();
            return Json(projectList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MakePayment()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            List<PaymentInformation> lstPayments = new List<PaymentInformation>();
            ViewBag.Payments = new SelectList(lstPayments, "BookingAmount", "CreatedDate");
            return View();
        }

        [HttpPost]
        public ActionResult MakePayment(PaymentInformation payInfo)
        {
            var result = booking.SaveNewPayment(payInfo);
            if (result)
                TempData["successmessage"] = "Payment Successfull";
            else
                TempData["successmessage"] = "Payment Failed";
            ModelState.Clear();
            //List<Projects> projectList1 = booking.BindProjects();
            //TempData["ProjectList"] = new SelectList(projectList1, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            List<PaymentInformation> lstPayments = new List<PaymentInformation>();
            ViewBag.Payments = new SelectList(lstPayments, "BookingAmount", "CreatedDate");
            return View();
        }

        public ActionResult GetPaymentDetails(int flatID)
        {
            List<PaymentInformation> lstPayments = booking.BindPaymentDetails(flatID);

            foreach (var item in lstPayments)
            {
                item.FormattedDate = Convert.ToDateTime(item.CreatedDate).Date.ToShortDateString();
            }
            return Json(lstPayments, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAgentPaymentDetails(int flatID)
        {
            List<AgentPaymentInformation> lstPayments = booking.BindAgentPaymentDetails(flatID);

            foreach (var item in lstPayments)
            {
                item.FormattedDate = Convert.ToDateTime(item.CreatedDate).Date.ToShortDateString();
            }
            return Json(lstPayments, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllProjects()
        {
            List<Projects> projectList = booking.BindAllProjects();
            return Json(projectList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetTowersInProgress(int ProjectId)
        {
            List<Towers> CityList = booking.BindTowersInProgress(ProjectId);
            return Json(CityList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetFlatsInProgress(int TowerId)
        {
            List<Flats> CityList = booking.BindFlatsInProgress(TowerId);
            return Json(CityList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MakeAgentPayment()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectListAgentPayments"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectListAgentPayments");
            //List<AgentPaymentInformation> lstPayments = new List<AgentPaymentInformation>();
            //ViewBag.Payments = new SelectList(lstPayments, "BookingAmount", "CreatedDate");
            return View();
        }

        [HttpPost]
        public ActionResult MakeAgentPayment(AgentPaymentInformation payInfo)
        {
            var result = booking.SaveNewAgentPayment(payInfo);
            if (result)
                TempData["successmessage"] = "Payment Successfull";
            else
                TempData["successmessage"] = "Payment Failed";
            ModelState.Clear();
            TempData.Keep("ProjectListAgentPayments");
            List<PaymentInformation> lstPayments = new List<PaymentInformation>();
            ViewBag.Payments = new SelectList(lstPayments, "BookingAmount", "CreatedDate");
            return View();
        }
    }
}