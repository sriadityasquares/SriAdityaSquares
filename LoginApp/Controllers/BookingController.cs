﻿using BusinessLayer;
using DataLayer;
using log4net;
using LoginApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ModelLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        CommonBL common = new CommonBL();
        BookingBL booking = new BookingBL();
        private static readonly ILog log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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

        public ActionResult New(int ProjectID = 0, int TowerID = 0, int FlatID = 0, string BookingStatus = "")
        {
            List<Country> countryList = common.BindCountry();
            List<Projects> projectList = booking.BindProjects();

            TempData["ProjectID"] = ProjectID;
            TempData["TowerID"] = TowerID;
            TempData["FlatID"] = FlatID;
            TempData["SchemeID"] = 0;
            TempData["AgentID"] = 0;
            ViewBag.ScreenName = "New Booking";
            TempData["CountryList"] = new SelectList(countryList, "CountryID", "CountryName");
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData["BookingDate"] = null;
            TempData.Keep("ProjectList");
            TempData.Keep("CountryList");
            if (BookingStatus != "" && BookingStatus != "O")
            {
                ModelLayer.BookingInformation b = new ModelLayer.BookingInformation();
                b = booking.GetBookingInformation(FlatID);
                if (b != null)
                {
                    TempData["SchemeID"] = b.SchemeID;
                    TempData["AgentID"] = b.AgentID;
                    TempData["FlatName"] = b.FlatName;
                    TempData["PayMode"] = b.PaymentModeID;
                    TempData["State"] = b.State;
                    TempData["City"] = b.City;
                    TempData["BookingStatus"] = "H";
                    TempData["BookingID"] = b.BookingID;
                    var bookingDate = Convert.ToDateTime(b.CreatedDate).ToString("MM/dd/yyyy");
                    //bookingDate
                    //DateTime dt = DateTime.ParseExact(b.CreatedDate.ToString(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    ////s will be MM/dd/yyyy format string
                    //string s = dt.ToString("MM/dd/yyyy");
                    TempData["BookingDate"] = bookingDate;
                    ViewBag.ScreenName = "Edit Booking";
                }
                return View(b);
            }
            return View();
        }



        [HttpPost]
        public ActionResult New(ModelLayer.BookingInformation b)
        {
            bool result = false;
            try
            {
                b.CreatedDate = Convert.ToDateTime(b.BookingDate);
                b.CreatedBy = User.Identity.Name;
                if (b.BookingID == Guid.Empty)
                {
                    result = booking.SaveNewBooking(b);
                }
                else
                {
                    result = booking.UpdateBooking(b);
                }
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

            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            if (result)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
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
            try
            {
                //payInfo.CreatedDate = DateTime.ParseExact(payInfo.PaymentDate, "dd/MM/yyyy", null);
                payInfo.CreatedBy = User.Identity.Name;
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
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
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

        public JsonResult GetFlatsExceptOpen(int TowerId)
        {
            List<Flats> flatList = booking.BindFlatsExceptOpen(TowerId);
            return Json(flatList, JsonRequestBehavior.AllowGet);

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
            try
            {
                if (payInfo.AgentNetPaid == null)
                {
                    payInfo.AgentNetPaid = 0;
                }
                if (payInfo.AgentNetBalance == null)
                {
                    payInfo.AgentNetBalance = 0;
                }
                payInfo.CreatedDate = Convert.ToDateTime(payInfo.AgentPaymentDate);
                payInfo.CreatedBy = User.Identity.Name;
                var result = booking.SaveNewAgentPayment(payInfo);
                if (result)
                    TempData["successmessage"] = "Payment Successfull";
                else
                    TempData["successmessage"] = "Payment Failed";
                ModelState.Clear();
                TempData.Keep("ProjectListAgentPayments");
                List<PaymentInformation> lstPayments = new List<PaymentInformation>();
                ViewBag.Payments = new SelectList(lstPayments, "BookingAmount", "CreatedDate");
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return View();
        }

        public ActionResult MakeAgentTotalPayment()
        {

            List<Agent> agentList = common.BindAgents();
            TempData["Agents"] = new SelectList(agentList, "AgentID", "AgentName");
            TempData.Keep("Agents");
            return View();
        }

        [HttpPost]
        public ActionResult MakeAgentTotalPayment(AgentTotalPayments payInfo)
        {
            payInfo.CreatedBy = User.Identity.Name;
            payInfo.CreatedDate = DateTime.Now.Date;
            var result = booking.SaveNewAgentTotalPayment(payInfo);
            if (result)
                TempData["successmessage"] = "Payment Successfull";
            else
                TempData["successmessage"] = "Payment Failed";
            ModelState.Clear();
            TempData.Keep("Agents");
            return View();
        }
        public JsonResult GetAgentTotalPayDetails(int AgentID)
        {
            AgentTotalPayments agentPay = booking.GetAgentTotalPay(AgentID);
            return Json(agentPay, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MakeFlatWiseAgentPayments()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            return View();
        }

        public JsonResult GetFlatWiseAgentCommission(int flatID)
        {
            //if(flatID == 0)
            //{
            //    flatID = 13;
            //}
            List<FlatWiseAgentCommission> fwac = booking.BindFlatWiseAgentCommission(flatID);
            return Json(fwac, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdateAgentFlatWisePayments(string models)
        {
            List<FlatWiseAgentCommission> data = JsonConvert.DeserializeObject<List<FlatWiseAgentCommission>>(models);
            var result = booking.UpdateAgentPayment(data[0]);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Selfie()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }

        [HttpPost]
        public ActionResult Selfie(ModelLayer.CustomerVisitInfo cv)
        {
            ////byte[] image;
            //using (Stream inputStream = cv.SelfieFile.InputStream)
            //{
            //    MemoryStream memoryStream = inputStream as MemoryStream;
            //    if (memoryStream == null)
            //    {
            //        memoryStream = new MemoryStream();
            //        inputStream.CopyTo(memoryStream);
            //    }
            //    cv.Selfie = memoryStream.ToArray();
            //}
            //string base64String = Convert.ToBase64String(image, 0, image.Length);
            //cv.se = "data:image/png;base64," + base64String;
            //Use Namespace called :  System.IO  
            string FileName = Path.GetFileNameWithoutExtension(cv.SelfieFile.FileName);

            //To Get File Extension  
            string FileExtension = Path.GetExtension(cv.SelfieFile.FileName);

            //Add Current Date To Attached File Name  
            FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;
            if (!Directory.Exists(Server.MapPath("~/Content/Images/Selfies")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Content/Images/Selfies"));
            }
            string path = "~/Content/Images/Selfies/";
            string UploadPath = Server.MapPath(path);
            //Get Upload path from Web.Config file AppSettings.  
            //string UploadPath = "~/Content/Images/";

            //Its Create complete path to store in server.  
            string savePath = UploadPath + FileName;

            //To copy and save file into server.  
            cv.SelfieFile.SaveAs(savePath);
            cv.SelfieURL = path.Remove(0, 1) + FileName;
            cv.AddedBy = User.Identity.Name;
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            cv.DateAdded = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            var result = booking.UploadSelfie(cv);
            if (result)
            {
                TempData["successmessage"] = "Selfie Uploaded Successfully";
            }
            else
            {
                TempData["successmessage"] = "Selfie Uploaded Failed";
            }
            ModelState.Clear();
            TempData.Keep("ProjectList");
            return View();
        }

        public ActionResult SelfiePoint()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }


        public JsonResult GetSelfies(int projectId)
        {
            var customerVisits = booking.GetSelfies(projectId);
            string html = "";
            int j = 0;
            for (int i = 0; i < customerVisits.Count; i = i + 3)
            {
                html = html + "<div class=\"row\">";
                for (j = 0; (i + j) < customerVisits.Count && j < 3; j++)
                {
                    html = html + "<div class=\"col-sm-4\"><div class=\"card bg-info text-white\"><img src = \"" + customerVisits[i + j].SelfieURL + "\" style =\"width:100%\" /><div class=\"containercard\"><h4><b>" + customerVisits[i + j].AgentName + "</b></h4><h6><b>CUSTOMER :" + customerVisits[i + j].CustomerName + "</b></h6><h6><b>PROJECT  :" + customerVisits[i + j].ProjectName + "</b></h6><h6><b>DATE     : " + customerVisits[i + j].DateAdded.ToString() + "</b></h6></div></div></div>";
                    //html.Replace("#url", customerVisits[i + j].SelfieURL);
                    //html.Replace("#AgentName", customerVisits[i + j].AgentName);
                    //html.Replace("#Customer", customerVisits[i + j].CustomerName);
                    //html.Replace("#Project", customerVisits[i + j].ProjectName);
                    //html.Replace("#Date", customerVisits[i + j].DateAdded.ToString());
                }
                html = html + "</div><br/>";
            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }
    }
}