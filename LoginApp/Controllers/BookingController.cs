using BusinessLayer;
using DataLayer;
using ExcelDataReader;
using log4net;
using LoginApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ModelLayer;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        [Authorize(Roles = "Admin,DataEntry,Franchise Owner")]
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
                    TempData["ChequeStatus"] = b.ChequeStatus;
                    TempData["State"] = b.State;
                    TempData["City"] = b.City;
                    TempData["BookingStatus"] = "P";
                    TempData["BookingID"] = b.BookingID;
                    var bookingDate = Convert.ToDateTime(b.CreatedDate).ToString("MM/dd/yyyy");

                    //b.DueDate = Convert.ToDateTime(b.DueDate).ToString("dd/MM/yyyy")
                    var chequeDate = "";
                    if (b.ChequeDate != null)
                    {
                        chequeDate = Convert.ToDateTime(b.ChequeDate).ToString("MM/dd/yyyy");
                    }
                    //bookingDate
                    //DateTime dt = DateTime.ParseExact(b.CreatedDate.ToString(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    ////s will be MM/dd/yyyy format string
                    //string s = dt.ToString("MM/dd/yyyy");
                    TempData["BookingDate"] = bookingDate;
                    TempData["CQDate"] = chequeDate;
                    ViewBag.ScreenName = "Edit Booking";
                }
                return View(b);
            }
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> New(ModelLayer.BookingInformation b)
        {
            bool result = false;
            try
            {
                b.ChequeDate = b.ChequeDateMod;
                b.CreatedDate = Convert.ToDateTime(b.BookingDate);
                b.CreatedBy = User.Identity.Name;
                var isNew = false;
                if (b.BookingID == Guid.Empty)
                {
                    isNew = true;
                    if (b.PaymentModeID == "1")
                        b.ChequeStatus = "Received";
                    else
                        b.ChequeStatus = "Not-Applicable";
                    if (User.IsInRole("Franchise Owner"))
                    {
                        result = booking.SaveNewFranchiseBooking(b);
                        isNew = false;
                    }
                    else
                    {
                        result = booking.SaveNewBooking(b);

                    }
                }
                else
                {
                    result = booking.UpdateBooking(b);
                }
                if (result)
                {
                    //ViewBag.result = "Booking Successfull";
                    TempData["successmessage"] = "Booking Successfull";
                    //var user = new ApplicationUser { UserName = b.Email, Email = b.Email };
                    if (isNew)
                    {

                        var user = new ApplicationUser { UserName = b.Email, Email = b.Email, PhoneNumber = b.Mobile.ToString() };
                        var result1 = await UserManager.CreateAsync(user, "Welcome@123");

                        if (result1.Succeeded)
                        {
                            var roleadd = await UserManager.AddToRoleAsync(user.Id, "Customer");

                            var message = "Welcome to Sri Aditya Squares" + Environment.NewLine + "Below are your login credetials:" + Environment.NewLine + "Username :" + b.Email + Environment.NewLine + "Password :" + "Welcome@123" + Environment.NewLine + "Please use the link to login to the application :" + "https://sasinfra.in";
                            var client = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + b.Mobile + "&smsContentType=english");
                            var request = new RestRequest(Method.GET);
                            request.AddHeader("Cache-Control", "no-cache");
                            IRestResponse response = client.Execute(request);

                            var client1 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + 8121751751 + "&smsContentType=english");
                            var request1 = new RestRequest(Method.GET);
                            request1.AddHeader("Cache-Control", "no-cache");
                            IRestResponse response1 = client1.Execute(request1);
                            SMS sms = new SMS();
                            sms.MessageType = "New Booking";
                            sms.Message = message;
                            sms.Recipients = b.Mobile + "," + "8121751751";
                            sms.CreatedBy = User.Identity.Name;
                            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                            var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                            //var sysDate = Convert.ToDateTime(indianTime).ToString("dd/MM/yyyy");
                            sms.CreatedDate = indianTime;
                            CommonBL common = new CommonBL();
                            common.LogSMS(sms);
                        }
                    }
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
            List<Towers> towerList = new List<Towers>();
            if (!User.IsInRole("Customer") && !User.IsInRole("Franchise Owner"))
            {
                towerList = booking.BindTowers(ProjectId);
            }
            else
            {
                if (User.IsInRole("Franchise Owner"))
                {
                    towerList = booking.BindFranchiseTowers(ProjectId, User.Identity.Name);
                }
                else
                {
                    towerList = booking.BindCustomerTowers(User.Identity.Name);

                }
            }
            return Json(towerList, JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetTowersforDashBoard(int ProjectId)
        {
            List<Towers> towerList = new List<Towers>();
            if (!User.IsInRole("Customer"))
            {
                towerList = booking.BindTowers(ProjectId);
            }
            else
            {
                towerList = booking.BindCustomerTowers(User.Identity.Name);
            }
            return Json(towerList, JsonRequestBehavior.AllowGet);

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

        [Authorize(Roles = "Admin,DataEntry")]
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


        [Authorize(Roles = "Admin,DataEntry,Franchise Owner")]
        public ActionResult MakePayment()
        {
            List<Projects> projectList = new List<Projects>();
            if (!User.IsInRole("Franchise Owner"))
            {
                projectList = booking.BindProjects();

            }
            else
            {
                projectList = booking.BindFranchiseProjects(User.Identity.Name);
            }
            @ViewBag.ProjectApproved = "Yes";
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            List<PaymentInformation> lstPayments = new List<PaymentInformation>();
            ViewBag.Payments = new SelectList(lstPayments, "BookingAmount", "CreatedDate");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,DataEntry,Franchise Owner")]
        public ActionResult MakePayment(PaymentInformation payInfo)
        {
            try
            {
                List<Projects> projectList = booking.BindProjects();
                TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
                TempData.Keep("ProjectList");
                @ViewBag.ProjectApproved = "Yes";
                if (payInfo.BookingAmount != null)
                {
                    if (Request.Form["GenerateReceipt"] == null)
                    {
                        //payInfo.CreatedDate = DateTime.ParseExact(payInfo.PaymentDate, "dd/MM/yyyy", null);
                        payInfo.CreatedBy = User.Identity.Name;
                        if (User.IsInRole("Franchise Owner"))
                            payInfo.ViewReceipt = false;
                        else
                            payInfo.ViewReceipt = true;
                        
                        if (payInfo.PaymentModeID == "1")
                            payInfo.ChequeStatus = "Received";
                        else
                            payInfo.ChequeStatus = "Not-Applicable";
                        var result = booking.SaveNewPayment(payInfo);
                        if (result)
                            TempData["successmessage"] = "Payment Successfull";
                        else
                            TempData["successmessage"] = "Payment Failed";
                    }
                    else
                    {
                        var payid = Request.Form["GenerateReceipt"];
                    }
                    ModelState.Clear();

                    TempData.Keep("ProjectList");
                    List<PaymentInformation> lstPayments = new List<PaymentInformation>();
                    ViewBag.Payments = new SelectList(lstPayments, "BookingAmount", "CreatedDate");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            ModelState.Clear();
            TempData.Keep("ProjectList");
            return View();
        }

        public ActionResult GetPaymentDetails(int flatID, bool backup = false)
        {
            List<PaymentInformation> lstPayments = new List<PaymentInformation>();
            if (!backup)
                lstPayments = booking.BindPaymentDetails(flatID);
            
            
            foreach (var item in lstPayments)
            {
                if (item.ChequeDate != null)
                    item.FormattedDate = Convert.ToDateTime(item.ChequeDate).Date.ToString("dd/MM/yyyy");
                else
                    item.FormattedDate = "";
                if (item.CreatedDate != null)
                    item.FormattedDate2 = Convert.ToDateTime(item.CreatedDate).Date.ToString("dd/MM/yyyy");
                else
                    item.FormattedDate2 = "";
                item.ViewReceipt = true;
            }
            return Json(lstPayments, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPaymentsForCancelledFlats(string BookingID)
        {
            List<PaymentInformation> lstPayments = new List<PaymentInformation>();
         
            lstPayments = booking.BindPaymentDetailsForCancelled(Guid.Parse(BookingID));

            foreach (var item in lstPayments)
            {
                if (item.ChequeDate != null)
                    item.FormattedDate = Convert.ToDateTime(item.ChequeDate).Date.ToShortDateString();
                else
                    item.FormattedDate = "";

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
            List<Flats> flatsList = new List<Flats>();
            if (!User.IsInRole("Franchise Owner"))
            {
                flatsList = booking.BindFlatsInProgress(TowerId);
            }
            else
            {
                flatsList = booking.BindFranchiseFlatsInProgress(TowerId, User.Identity.Name);
            }
            return Json(flatsList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetFlatsExceptOpen(int TowerId)
        {
            List<Flats> flatList = new List<Flats>();
            if (User.IsInRole("Customer"))
            {
                flatList = booking.BindCustomerFlats(User.Identity.Name);
            }
            else
            {
                if (!User.IsInRole("Franchise Owner"))
                {
                    flatList = booking.BindFlatsExceptOpen(TowerId);
                }
                else
                {
                    flatList = booking.BindFranchiseFlatsInProgress(TowerId, User.Identity.Name);
                }
            }
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
            payInfo.CreatedDate = DateTime.Now;
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

        [Authorize(Roles = "Admin,Agent,Customer,DataEntry")]
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
            var isValid = booking.CheckDuplicateMobile(cv);
            if (isValid)
            {
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
            }
            else
            {
                TempData["successmessage"] = "Selfie with customer already exists!";
            }
            ModelState.Clear();
            TempData.Keep("ProjectList");
            return View();
        }

        [Authorize(Roles = "Admin,Agent,Customer,DataEntry")]
        public ActionResult SelfiePoint()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }


        public JsonResult GetSelfies(int projectId, string Mobile)
        {
            var customerVisits = booking.GetSelfies(projectId, Mobile);
            string html = "";
            int j = 0;
            for (int i = 0; i < customerVisits.Count; i = i + 3)
            {
                html = html + "<div class=\"row\">";
                for (j = 0; (i + j) < customerVisits.Count && j < 3; j++)
                {
                    html = html + "<div class=\"col-sm-4\"><div class=\"card bg-info text-white\"><img  src = \"" + customerVisits[i + j].SelfieURL + "\" style =\"width:100%;height:400px;object-fit: contain;\" /><div class=\"containercard\"><h4><b>" + customerVisits[i + j].AgentName + "</b></h4><h6><b>CUSTOMER :" + customerVisits[i + j].CustomerName + "</b></h6><h6><b>PROJECT  :" + customerVisits[i + j].ProjectName + "</b></h6><h6><b>DATE     : " + Convert.ToDateTime(customerVisits[i + j].DateAdded).ToString("dd/MM/yyyy") + "</b></h6></div></div></div>";

                }
                html = html + "</div><br/>";
            }
            if (html == "")
            {
                html = "<span> No Records Found </span>";
            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Agent,Customer,DataEntry")]
        public ActionResult AddSiteVist()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }

        [HttpPost]
        public ActionResult AddSiteVist(SiteVisitInfo siteVisitInfo)
        {

            siteVisitInfo.TermsAccepted = true;
            siteVisitInfo.CreatedBy = User.Identity.Name;
            siteVisitInfo.CreatedDate = DateTime.Now;
            var result = booking.AddSiteVisit(siteVisitInfo, User.Identity.Name);
            if (result)
            {
                TempData["successmessage"] = "Site Visit Added Successfully";


            }
            else
            {
                TempData["successmessage"] = "Site Visit Adding Failed";
            }
            ModelState.Clear();


            TempData.Keep("ProjectList");
            return View();

        }

        public JsonResult GetMySiteVisits()
        {
            string username = User.IsInRole("Admin") || User.Identity.Name == "nsrinivas78@gmail.com" ? null : User.Identity.Name;
            var result = booking.GetMySiteVisits(username);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [Authorize(Roles = "Admin")]
        public ActionResult SiteVisitApproval()
        {
            return View();
        }

        public JsonResult GetSiteVisitsForApproval()
        {

            var result = booking.GetSiteVisitsForApproval();
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult UpdateSiteVisitApproval(string models)
        {
            List<SiteVisitInfo> data = JsonConvert.DeserializeObject<List<SiteVisitInfo>>(models);
            data[0].isApproved = data[0].Status == "Approved" ? true : false;
            data[0].ModifiedBy = User.Identity.Name;
            data[0].ModifiedDate = DateTime.Now;
            data[0].ApprovedOrRejectedBy = User.Identity.Name;
            var result = booking.UpdateSiteVisitApproval(data[0], User.Identity.Name);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [Authorize(Roles = "Admin,DataEntry,Franchise Owner")]
        public ActionResult Invoice()
        {
            List<Projects> projectList = new List<Projects>();
            if (!User.IsInRole("Franchise Owner"))
            {
                projectList = booking.BindProjects();
            }
            else
            {
                projectList = booking.BindFranchiseProjects(User.Identity.Name);
            }

            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }

        public JsonResult GetReceiptDetails(int flatID)
        {
            var result = booking.GetBookingInformation(flatID);
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            var sysDate = Convert.ToDateTime(indianTime).ToString("dd/MM/yyyy");
            var bookingDate = result.ChequeDate != null ? Convert.ToDateTime(result.ChequeDate).ToString("dd/MM/yyyy") : "";
            var amountInWords = Helper.AmountInWords(Convert.ToDouble(result.BookingAmount));
            var bookID = result.BookingID.ToString().Split('-')[0].ToString().ToUpper();
            var html = "";
            if (!User.IsInRole("Franchise Owner"))
            {
                html = "<article><address></address><table class=\"meta\"><tr><th><span>Receipt  #</span></th><td><span contenteditable></span></td> </tr><tr><th><span>Date</span></th><td><span contenteditable>#BookingDate</span></td></tr><tr></tr></table><table class=\"inventory\"><tr><th><span>PROJECT :</span></th><td><span>#Project</span></td><th><span>TOWER :</span></th><td><span>#Tower</span></td></tr><tr><th><span>NAME:</span></th><td><span>#CName</span></td><th><span>MOBILE :</span></th><td><span>#CMobile</span></td></tr><tr><th><span>AMOUNT PAID:</span></th><td><span>#BookingAmount</span></td><th><span>FLAT/PLOT NO :</span></th><td><span>#Flat</span></td></tr><tr><th><span>SFT:</span></th><td><span contenteditable>#SFT</span></td><th><span>MODE :</span></th><td><span>#Cheque</span></td></tr><tr><th><span>Ref No:</span></th><td><span>#RefNo</span></td></tr></table></article>" +
               "<p> Remarks : #Details</p>";
                //var html = "<article><table class=\"inventory\"><tr><th>Project</th><td>#Project</td><th>Tower</ th ><td>#Tower</td></tr><tr><th>Name</th><td colspan = '3'>#CName</td></tr><tr><th>Mobile</th><td contenteditable>#CMobile</td><th>Amount Paid</th><td>#BookingAmount</td></tr><tr><th>Flat/Plot No</th><td>#Flat</td><th>Payment Date</th><td contenteditable>#BookingDate</td></tr><tr><th >Sft</th><td>#SFT</td><th>Bhk</th><td>#BHK</td></tr><tr><th>Mode</th><td contenteditable>#Cheque</td><th>Ref No</th><td contenteditable >#RefNo</td></tr> <tr><th> Amount in Words </th><td contenteditable colspan = '3' >#amountInWords</td></tr><tr><th> Bank Name </th><td contenteditable colspan = '3' > &nbsp;</td> </tr></table></article>";
                html = html.Replace("#BookingDate", bookingDate).Replace("#Project", result.ProjectName).Replace("#Tower", result.TowerName).Replace("#CName", result.Name).Replace("#CMobile", result.Mobile == null ? "" : result.Mobile.ToString()).Replace("#BookingAmount", "Rs. " + result.BookingAmount.ToString()).Replace("#Flat", result.FlatName).Replace("#SFT", result.Area.ToString()).Replace("#Cheque", result.PaymentMode.ToString()).Replace("#RefNo", result.ReferenceNo).Replace("#BHK", result.Bhk.ToString()).Replace("#Date", sysDate).Replace("#amountInWords", amountInWords).Replace("#bookingID", bookID).Replace("#Details", result.Remarks);

            }
            else
            {
                var registerNo = booking.GetFranchiseNoWithFlatID(flatID);

                html = "<article><address></address><table class=\"meta\"><tr><th><span>Receipt  #</span></th><td><span contenteditable></span></td> </tr><tr><th><span>Date</span></th><td><span contenteditable>#BookingDate</span></td></tr><tr></tr><tr><th>FranchiseNo</th><td><span>#FranchiseNo</span></td></tr></table><table class=\"inventory\"><tr><th><span>PROJECT :</span></th><td><span>#Project</span></td><th><span>TOWER :</span></th><td><span>#Tower</span></td></tr><tr><th><span>NAME:</span></th><td><span>#CName</span></td><th><span>MOBILE :</span></th><td><span>#CMobile</span></td></tr><tr><th><span>AMOUNT PAID:</span></th><td><span>#BookingAmount</span></td><th><span>FLAT/PLOT NO :</span></th><td><span>#Flat</span></td></tr><tr><th><span>SFT:</span></th><td><span contenteditable>#SFT</span></td><th><span>MODE :</span></th><td><span>#Cheque</span></td></tr><tr><th><span>Ref No:</span></th><td><span>#RefNo</span></td></tr></table></article>" +
              "<p> Remarks : #Details</p>";
                //var html = "<article><table class=\"inventory\"><tr><th>Project</th><td>#Project</td><th>Tower</ th ><td>#Tower</td></tr><tr><th>Name</th><td colspan = '3'>#CName</td></tr><tr><th>Mobile</th><td contenteditable>#CMobile</td><th>Amount Paid</th><td>#BookingAmount</td></tr><tr><th>Flat/Plot No</th><td>#Flat</td><th>Payment Date</th><td contenteditable>#BookingDate</td></tr><tr><th >Sft</th><td>#SFT</td><th>Bhk</th><td>#BHK</td></tr><tr><th>Mode</th><td contenteditable>#Cheque</td><th>Ref No</th><td contenteditable >#RefNo</td></tr> <tr><th> Amount in Words </th><td contenteditable colspan = '3' >#amountInWords</td></tr><tr><th> Bank Name </th><td contenteditable colspan = '3' > &nbsp;</td> </tr></table></article>";
                html = html.Replace("#BookingDate", bookingDate).Replace("#Project", result.ProjectName).Replace("#Tower", result.TowerName).Replace("#CName", result.Name).Replace("#CMobile", result.Mobile == null ? "" : result.Mobile.ToString()).Replace("#BookingAmount", "Rs. " + result.BookingAmount.ToString()).Replace("#Flat", result.FlatName).Replace("#SFT", result.Area.ToString()).Replace("#Cheque", result.PaymentMode.ToString()).Replace("#RefNo", result.ReferenceNo).Replace("#BHK", result.Bhk.ToString()).Replace("#Date", sysDate).Replace("#amountInWords", amountInWords).Replace("#bookingID", bookID).Replace("#Details", result.Remarks).Replace("#FranchiseNo", registerNo.ToString());

            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,DataEntry")]
        public ActionResult PaymentReceipts()
        {
            List<Projects> projectList = booking.BindProjects();
            //ViewBag.BackgroundURL = "letterhead_new.jpg";
            TempData["BackgroundURL"] = "";
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }


        public JsonResult GetPaymentReceipts(int paymentID)
        {
            var result = booking.GetPaymentInformation(paymentID);


            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            var sysDate = Convert.ToDateTime(indianTime).ToString("dd/MM/yyyy");
            //var payDate = Convert.ToDateTime(result.ChequeDate).ToString("dd/MM/yyyy");
            var payDate = result.ChequeDate != null ? Convert.ToDateTime(result.ChequeDate).ToString("dd/MM/yyyy") : "";
            var amountInWords = Helper.AmountInWords(Convert.ToDouble(result.BookingAmount));
             
            var html = "";
            bool projectApprovalStatus = booking.GetProjectApprovalStatus(result.PaymentID);
            ViewBag.ProjectApproved = projectApprovalStatus?"Yes":"No";
            if(projectApprovalStatus)
                @ViewBag.Background = "../../Content/Images/letterhead_new.jpg";
            else
                @ViewBag.Background = "";
            if (!projectApprovalStatus)
            {
                html = "<article><address></address><table class=\"meta\"><tr><th> Pay ID:</th><td><span contenteditable>#PaymentID</span></td></tr><tr><th>Date</th><td><span contenteditable>#Date</span></td></tr></tbody></table><table class=\"inventory\"><tbody><tr><th>Project</th><td>#Project</td><th>Tower</th><td>#Tower</td></tr><tr><th>Name</th><td colspan = '3'>#CName</td></tr><th>Mobile</th><td contenteditable>#CMobile</td><th>Amount Paid</th><td>#BookingAmount</td></tr><tr><th>Flat/Plot No</th><td>#Flat</td><th>Payment Date</th><td contenteditable>#PaymentDate</td></tr><tr><th >Sft</th><td>#SFT</td><th>Bhk</th><td>#BHK</td></tr><tr><th>Mode</th><td contenteditable>#Cheque</td><th>Ref No</th><td><span contenteditable>#RefNo</span></td></tr><tr><th>Amount in Words</th><td colspan = '3' contenteditable>#amountInWords</td></tr><tr><th>Bank Name</th><td colspan = '3' contenteditable></td></tr><tr><th>Details</th><td colspan = '3' contenteditable>#Details</td></tr></table></article>";
                html = html.Replace("#Project", result.ProjectName).Replace("#Tower", result.TowerName).Replace("#CName", result.Name).Replace("#CMobile", result.Mobile == null ? "" : result.Mobile.ToString()).Replace("#BookingAmount", "Rs. " + result.BookingAmount.ToString()).Replace("#Flat", result.FlatName).Replace("#SFT", result.Area.ToString()).Replace("#Cheque", result.PaymentMode.ToString()).Replace("#RefNo", result.ReferenceNo).Replace("#PaymentID", result.PaymentID.ToString()).Replace("#PaymentDate", payDate).Replace("#Date", sysDate).Replace("#amountInWords", amountInWords).Replace("#BHK", result.Bhk.ToString()).Replace("#Details", result.Details);

            }
            else
            {
                if (!User.IsInRole("Franchise Owner"))
                {
                    html = "<article><address></address><table class=\"meta\"><tr><th> Pay ID:</th><td><span contenteditable>#PaymentID</span></td></tr><tr><th>Date</th><td><span contenteditable>#Date</span></td></tr></tbody></table><table class=\"inventory\"><tbody><tr><th>Project</th><td>#Project</td><th>Tower</th><td>#Tower</td></tr><tr><th>Name</th><td colspan = '3'>#CName</td></tr><th>Mobile</th><td contenteditable>#CMobile</td><th>Amount Paid</th><td>#BookingAmount</td></tr><tr><th>Flat/Plot No</th><td>#Flat</td><th>Payment Date</th><td contenteditable>#PaymentDate</td></tr><tr><th >Sft</th><td>#SFT</td><th>Bhk</th><td>#BHK</td></tr><tr><th>Mode</th><td contenteditable>#Cheque</td><th>Ref No</th><td><span contenteditable>#RefNo</span></td></tr><tr><th>Amount in Words</th><td colspan = '3' contenteditable>#amountInWords</td></tr><tr><th>Bank Name</th><td colspan = '3' contenteditable></td></tr><tr><th>Details</th><td colspan = '3' contenteditable>#Details</td></tr></table></article>";
                    html = html.Replace("#Project", result.ProjectName).Replace("#Tower", result.TowerName).Replace("#CName", result.Name).Replace("#CMobile", result.Mobile == null ? "" : result.Mobile.ToString()).Replace("#BookingAmount", "Rs. " + result.BookingAmount.ToString()).Replace("#Flat", result.FlatName).Replace("#SFT", result.Area.ToString()).Replace("#Cheque", result.PaymentMode.ToString()).Replace("#RefNo", result.ReferenceNo).Replace("#PaymentID", result.PaymentID.ToString()).Replace("#PaymentDate", payDate).Replace("#Date", sysDate).Replace("#amountInWords", amountInWords).Replace("#BHK", result.Bhk.ToString()).Replace("#Details", result.Details);
                }
                else
                {
                    var registerNo = booking.GetFranchiseNoWithPaymentID(paymentID);
                    html = "<article><address></address><table class=\"meta\"><tr><th> Pay ID:</th><td><span contenteditable>#PaymentID</span></td></tr><tr><th>Date</th><td><span contenteditable>#Date</span></td></tr><tr><th>FranchiseNo</th><td><span>#FranchiseNo</span></td></tr></tbody></table><table class=\"inventory\"><tbody><tr><th>Project</th><td>#Project</td><th>Tower</th><td>#Tower</td></tr><tr><th>Name</th><td colspan = '3'>#CName</td></tr><th>Mobile</th><td contenteditable>#CMobile</td><th>Amount Paid</th><td>#BookingAmount</td></tr><tr><th>Flat/Plot No</th><td>#Flat</td><th>Payment Date</th><td contenteditable>#PaymentDate</td></tr><tr><th >Sft</th><td>#SFT</td><th>Bhk</th><td>#BHK</td></tr><tr><th>Mode</th><td contenteditable>#Cheque</td><th>Ref No</th><td><span contenteditable>#RefNo</span></td></tr><tr><th>Amount in Words</th><td colspan = '3' contenteditable>#amountInWords</td></tr><tr><th>Bank Name</th><td colspan = '3' contenteditable></td></tr><tr><th>Details</th><td colspan = '3' contenteditable>#Details</td></tr></table></article>";
                    html = html.Replace("#Project", result.ProjectName).Replace("#Tower", result.TowerName).Replace("#CName", result.Name).Replace("#CMobile", result.Mobile == null ? "" : result.Mobile.ToString()).Replace("#BookingAmount", "Rs. " + result.BookingAmount.ToString()).Replace("#Flat", result.FlatName).Replace("#SFT", result.Area.ToString()).Replace("#Cheque", result.PaymentMode.ToString()).Replace("#RefNo", result.ReferenceNo).Replace("#PaymentID", result.PaymentID.ToString()).Replace("#PaymentDate", payDate).Replace("#Date", sysDate).Replace("#amountInWords", amountInWords).Replace("#BHK", result.Bhk.ToString()).Replace("#Details", result.Details).Replace("#FranchiseNo", registerNo.ToString());

                }
            }
            var jsonresult = new { data = html, status = projectApprovalStatus };
            return Json(jsonresult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPaymentReceiptsForCancelledFlats(int paymentID)
        {
            var result = booking.GetPaymentInformation(paymentID);


            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            var sysDate = Convert.ToDateTime(indianTime).ToString("dd/MM/yyyy");
            //var payDate = Convert.ToDateTime(result.ChequeDate).ToString("dd/MM/yyyy");
            var payDate = result.ChequeDate != null ? Convert.ToDateTime(result.ChequeDate).ToString("dd/MM/yyyy") : "";
            var amountInWords = Helper.AmountInWords(Convert.ToDouble(result.BookingAmount));
            var html = "<article><address></address><table class=\"meta\"><tr><th> Pay ID:#</th><td><span contenteditable>#PaymentID</span></td></tr><tr><th>Date</th><td><span contenteditable>#Date</span></td></tr></tbody></table><table class=\"inventory\"><tbody><tr><th>Project</th><td>#Project</td><th>Tower</th><td>#Tower</td></tr><tr><th>Name</th><td colspan = '3'>#CName</td></tr><th>Mobile</th><td contenteditable>#CMobile</td><th>Amount Paid</th><td>#BookingAmount</td></tr><tr><th>Flat/Plot No</th><td>#Flat</td><th>Payment Date</th><td contenteditable>#PaymentDate</td></tr><tr><th >Sft</th><td>#SFT</td><th>Bhk</th><td>#BHK</td></tr><tr><th>Mode</th><td contenteditable>#Cheque</td><th>Ref No</th><td><span contenteditable>#RefNo</span></td></tr><tr><th>Amount in Words</th><td colspan = '3' contenteditable>#amountInWords</td></tr><tr><th>Bank Name</th><td colspan = '3' contenteditable></td></tr><tr><th>Details</th><td colspan = '3' contenteditable>#Details</td></tr></table></article>";
            html = html.Replace("#Project", result.ProjectName).Replace("#Tower", result.TowerName).Replace("#CName", result.Name).Replace("#CMobile", result.Mobile == null ? "" : result.Mobile.ToString()).Replace("#BookingAmount", "Rs. " + result.BookingAmount.ToString()).Replace("#Flat", result.FlatName).Replace("#SFT", result.Area.ToString()).Replace("#Cheque", result.PaymentMode.ToString()).Replace("#RefNo", result.ReferenceNo).Replace("#PaymentID", result.PaymentID.ToString()).Replace("#PaymentDate", payDate).Replace("#Date", sysDate).Replace("#amountInWords", amountInWords).Replace("#BHK", result.Bhk.ToString()).Replace("#Details", result.Details);
            return Json(html, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult ClientPayments()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }

        [HttpPost]
        public ActionResult ClientPayments(ClientPayments clientPayments)
        {

            clientPayments.CreatedBy = User.Identity.Name;
            clientPayments.CreatedDate = DateTime.Now;
            var result = booking.AddClientPayments(clientPayments);
            if (result)
            {
                TempData["successmessage"] = "Payment Added Successfully";

            }
            else
            {
                TempData["successmessage"] = "Payment Adding Failed";
            }
            ModelState.Clear();
            TempData.Keep("ProjectList");
            return View();
        }

        public JsonResult GetClientPayments()
        {
            var result = booking.GetClientPayments();
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [Authorize(Roles = "Admin")]
        public ActionResult DailyExpenses()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");

            List<ProjectExpenseCategory> projectexpCatList = booking.BindProjectExpenseCategory();
            TempData["ExpenseCategoryList"] = new SelectList(projectexpCatList, "ID", "SubCategory");
            TempData.Keep("ExpenseCategoryList");
            return View();
        }

        [HttpPost]
        public ActionResult DailyExpenses(DailyExpense dailyExpense)
        {

            dailyExpense.CreatedBy = User.Identity.Name;
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            dailyExpense.CreatedDate = Convert.ToDateTime(indianTime).ToString("MM/dd/yyyy");
            //dailyExpense.ExpenseDate = Convert.ToDateTime(dailyExpense.ExpenseDate).ToString("MM/dd/yyyy");
            dailyExpense.ExpenseDate = DateTime.ParseExact(dailyExpense.ExpenseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                         .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (dailyExpense.ProjectID == null)
                dailyExpense.ProjectName = "";
            var result = false;
            List<Projects> projectList = booking.BindProjects();
            List<ProjectExpenseCategory> projectexpCatList = booking.BindProjectExpenseCategory();
            if (dailyExpense.BulkUpload != null)
            {
                try
                {
                    var lstdailyExpenses = GetDataFromCSVFile(dailyExpense.BulkUpload.InputStream, projectList, projectexpCatList);
                    result = booking.BulkUploadExpenses(lstdailyExpenses);
                    ModelState.Clear();
                    //return Json(new { Status = 1, Message = "Intents Uploaded Successfully " });
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    result = false;
                    //return Json(new { Status = 0, Message = ex.Message });
                }
            }

            else
            {
                result = booking.AddDailyExpense(dailyExpense);
            }
            if (result)
            {
                TempData["successmessage"] = "Expense Added Successfully";

            }
            else
            {
                TempData["successmessage"] = "Expense Adding Failed";
            }
            ModelState.Clear();
           
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            
            TempData["ExpenseCategoryList"] = new SelectList(projectexpCatList, "ID", "SubCategory");
            TempData.Keep("ExpenseCategoryList");
            return View();
        }

        private List<DailyExpense> GetDataFromCSVFile(Stream stream,List<Projects> projects,List<ProjectExpenseCategory> projectExpenseCategory)
        {
            var lstExpenses = new List<DailyExpense>();
            try
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // To set First Row As Column Names  
                        }
                    });

                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];
                        foreach (DataRow objDataRow in dataTable.Rows)
                        {
                            if (objDataRow.ItemArray.All(x => string.IsNullOrEmpty(x?.ToString()))) continue;
                            lstExpenses.Add(new DailyExpense()
                            {
                                
                            ExpenseDate = DateTime.ParseExact(objDataRow["ExpenseDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                                PaidTo = objDataRow["PaidTo"].ToString(),
                                Particulars = objDataRow["Particulars"].ToString(),
                                ExpenseType = objDataRow["ExpenseType"].ToString(),
                                ProjectName = objDataRow["ProjectName"].ToString(),
                                ProjectID = projects.Where(x => x.ProjectName == objDataRow["ProjectName"].ToString()).Select(x => x.ProjectID).FirstOrDefault(),
                                SubCategory = objDataRow["SubCategory"].ToString(),
                                SubCategoryID = projectExpenseCategory.Where(x => x.SubCategory == objDataRow["SubCategory"].ToString()).Select(x => x.ID).FirstOrDefault(),
                                Amount = Convert.ToInt32(objDataRow["Amount"]),
                                PaymentMode = objDataRow["PaymentMode"].ToString(),
                                Comments = objDataRow["Comments"].ToString(),
                                CreatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
                                CreatedBy = User.Identity.Name

                            }); ;
                        }
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstExpenses;
        }
        public JsonResult GetDailyExpenses()
        {
            var result = booking.GetDailyExpenses();
           
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult UpdateDailyExpenses(string models)
        {

            List<DailyExpense> data = JsonConvert.DeserializeObject<List<DailyExpense>>(models);
            var result = booking.UpdateDailyExpenses(data[0]);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CancelBooking()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }

        [HttpGet]
        public JsonResult UpdateCancellation(string models)
        {
            try
            {
                List<Cancellation> data = JsonConvert.DeserializeObject<List<Cancellation>>(models);
                var result = booking.UpdateCancellation(data[0].Comments, data[0].ID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult UpdatePaymentDetails(int payID, string Details, string Ref)
        {
            try
            {
                //List<Cancellation> data = JsonConvert.DeserializeObject<List<Cancellation>>(models);
                var result = booking.UpdatePaymentDetails(payID, Details, Ref);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BookingCancellation(int flatID, string comments)
        {
            var result = booking.CancelBooking(flatID, comments);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCancellations()
        {
            var result = booking.GetCancellations();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Cheque()
        {
            return View();
        }

        public JsonResult GetChequeInfo()
        {
            var result = booking.GetChequeInfo();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateChequeStatus(string models)
        {
            List<GetChequeInfo> data = JsonConvert.DeserializeObject<List<GetChequeInfo>>(models);
            var result = booking.UpdateChequeInfo(data[0]);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Agreements()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }

        [HttpPost]
        public ActionResult Agreements(Agreements agreements)
        {
            agreements.CreatedBy = User.Identity.Name;
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            agreements.CreatedDate = Convert.ToDateTime(indianTime);
            var result = booking.AddAgreement(agreements);
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            if (result)
            {
                TempData["successmessage"] = "Agreement Added Successfully";

            }
            else
            {
                TempData["successmessage"] = "Agreement Adding Failed";
            }
            ModelState.Clear();
            return View();
        }


        public JsonResult GetAgreements()
        {
            var result = booking.GetAgreements();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public JsonResult DeleteAgreement(string models)
        {
            List<Agreements> data = JsonConvert.DeserializeObject<List<Agreements>>(models);
            var result = booking.DeleteAgreement(data[0].AgreementID);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteDailyExpense(string models)
        {
            List<DailyExpense> data = JsonConvert.DeserializeObject<List<DailyExpense>>(models);
            var result = booking.DeleteDailyExpense(data[0].ExpenseID);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteClientPayment(string models)
        {
            List<ClientPayments> data = JsonConvert.DeserializeObject<List<ClientPayments>>(models);
            var result = booking.DeleteClientPayment(data[0].ClientPayID);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Offers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportFile(HttpPostedFileBase importFile)
        {
            if (importFile == null) return Json(new { Status = 0, Message = "No File Selected" });

            try
            {
                Bitmap b = (Bitmap)Bitmap.FromStream(importFile.InputStream);
                ModelState.Clear();
                string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\Images\Offers.png"/*+importFile.FileName.Split('.')[1].ToString()*/);
                b.Save(file, ImageFormat.Png);
                return Json(new { Status = 1, Message = "Offers Uploaded Successfully " });

            }
            catch (Exception ex)
            {
                ModelState.Clear();
                return Json(new { Status = 0, Message = ex.Message });
            }
        }


        public ActionResult CheckList()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }

        public JsonResult GetCheckList(int flatID, bool backup = false)
        {
            ModelLayer.BookingInformation result = new ModelLayer.BookingInformation();
            var html = "";
            result = booking.GetBookingInformation(flatID);
            //else
            //result = booking.GetBookingInformationForCancelledFlats(flatID);
            //result = new ModelLayer.BookingInformation();
            if (result != null)
            {
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                CultureInfo inr = new CultureInfo("hi-IN");
                var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                var sysDate = Convert.ToDateTime(indianTime).ToString("dd/MM/yyyy");
                var bookingDate = result.ChequeDate != null ? Convert.ToDateTime(result.ChequeDate).ToString("dd/MM/yyyy") : "";
                var amountInWords = Helper.AmountInWords(Convert.ToDouble(result.BookingAmount));
                var bookID = result.BookingID.ToString().Split('-')[0].ToString().ToUpper();
                //var html = "<article><address></address><table class=\"meta\"><tr><th><span>Receipt  #</span></th><td><span contenteditable></span></td> </tr><tr><th><span>Date</span></th><td><span contenteditable>#BookingDate</span></td></tr><tr></tr></table><table class=\"inventory\"><tr><th><span>PROJECT :</span></th><td><span>#Project</span></td><th><span>TOWER :</span></th><td><span>#Tower</span></td></tr><tr><th><span>NAME:</span></th><td><span>#CName</span></td><th><span>MOBILE :</span></th><td><span>#CMobile</span></td></tr><tr><th><span>AMOUNT PAID:</span></th><td><span>#BookingAmount</span></td><th><span>FLAT/PLOT NO :</span></th><td><span>#Flat</span></td></tr><tr><th><span>SFT:</span></th><td><span contenteditable>#SFT</span></td><th><span>MODE :</span></th><td><span>#Cheque</span></td></tr><tr><th><span>Ref No:</span></th><td><span>#RefNo</span></td></tr></table></article>";
                html = "<table id='tblBookingDetails' class=\"table table-condensed\"><tr><th>Name</th><td>#CName</td><th>Contact</th><td>#Mobile</td></tr><tr><th>Aadhar</th><td>#Aadhar</td><th>Pan No</th><td>#PanNo</td></tr><tr><th>Project</th><td>#Project</td><th>Scheme</th><td>#SchemePercentage %</td></tr><tr><th>Flat/Plot No</th><td>#Flat</td><th>Sft Rate</th><td>#SftRate</td></tr><tr><th>Sft</th><td>#SFT</td><th>Bhk</th><td>#BHK</td></tr><tr><th>High Rise</th><td>#HIGHRISE</td><th>Total Rate</th><td><b>Rs.</b> #totalRate</td></tr> <tr><th>Discount</th><td>#discount</td><th>Final Rate</th><td><b>Rs.</b> #finalRate</td></tr><tr><th>ClubHouse</th><td><b>Rs.</b> #ClubHouse</td><th>Oth Charges</th><td><b>Rs.</b> #OtherCharges</td></tr><tr><th>Total</th><td><b>Rs.</b> #GrandRate</td><th>Scheme Due</th><td><b>Rs.</b> #SchemeDue</td></tr><tr><th>IBO Name</th><td>#AgentName</td><th>Booking Date</th><td>#BookingDate</td></tr><tr><th>Remarks</th><td>#Remarks</td><th>Facing</th><td>#Facing</td></tr><tr><th>IBO Share</th><td><b>Rs.</b> #IBOShare</td><th>Comp Share</th><td><b>Rs.</b> #Company</td></tr></table>";
                html = html.Replace("#CName", result.Name)
                    .Replace("#Project", result.ProjectName)
                    .Replace("#Tower", result.TowerName)
                    .Replace("#Aadhar", result.Aadhar == null ? "" : result.Aadhar.ToString())
                    .Replace("#PanNo", result.Pan == null ? "" : result.Pan.ToString())
                    .Replace("#Mobile", result.Mobile == null ? "" : result.Mobile.ToString())
                    .Replace("#BookingDate", result.ChequeDate != null ? Convert.ToDateTime(result.ChequeDate).ToString("dd/MM/yyyy") : "")
                    .Replace("#Flat", result.TowerName + "-" + result.FlatName)
                    .Replace("#SFT", result.Area.ToString())
                    .Replace("#SchemePercentage", result.SchemePercentage.ToString())
                    .Replace("#Address", result.Address == null ? "" : result.Address.ToString())
                    .Replace("#Cheque", result.PaymentMode.ToString())
                    .Replace("#RefNo", result.ReferenceNo)
                    .Replace("#BHK", result.Bhk.ToString())
                    .Replace("#GrandRate", string.Format(inr, "{0:#,#}", result.GrandRate))
                    .Replace("#finalRate", string.Format(inr, "{0:#,#}", result.FinalRate))
                    .Replace("#discount", result.Discount.ToString())
                    .Replace("#Remarks", result.Remarks)
                    .Replace("#HIGHRISE", result.HighRiseCharges.ToString())
                    .Replace("#totalRate", result.TotalRate.ToString())
                    .Replace("#Facing", result.Facing.ToString())
                    //.Replace("#Date", sysDate)
                    //.Replace("#amountInWords", amountInWords)
                    //.Replace("#bookingID", bookID)
                    .Replace("#Company", result.CompanyShare.ToString() != "0" ? result.CompanyShare.ToString("#,#", new CultureInfo(0x0439)) : "0")
                    .Replace("#IBOShare", result.IBOShare.ToString() != "0" ? result.IBOShare.ToString("#,#", new CultureInfo(0x0439)) : "0")
                    .Replace("#SchemeDue", result.DueAmount.ToString() != "0" ? result.DueAmount.ToString("#,#", new CultureInfo(0x0439)) : "0")
                    //.Replace("#GrandRate", "<b>Rs.</b>" + result.GrandRate.ToString())
                    .Replace("#SftRate", result.SftRate.ToString())
                    .Replace("#OtherCharges", result.OtherCharges.ToString() != "0" ? string.Format(inr, "{0:#,#}", result.OtherCharges) : "0")
                    .Replace("#ClubHouse", result.ClubHouseCharges.ToString() != "0" ? string.Format(inr, "{0:#,#}", result.ClubHouseCharges) : "0")
                    .Replace("#AgentName", result.AgentName);
                if (result.GSTAmount != null)
                {
                    html = html + "<p style='color:red;'>*Total amount is inclusive of 5% GST*</p>";
                }
            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCheckListForCancelledFlats(string BookingID)
        {
            var result = booking.GetBookingInformationForCancelledFlats(Guid.Parse(BookingID));
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            CultureInfo inr = new CultureInfo("hi-IN");
            var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            var sysDate = Convert.ToDateTime(indianTime).ToString("dd/MM/yyyy");
            var bookingDate = result.ChequeDate != null ? Convert.ToDateTime(result.ChequeDate).ToString("dd/MM/yyyy") : "";
            var amountInWords = Helper.AmountInWords(Convert.ToDouble(result.BookingAmount));
            var bookID = result.BookingID.ToString().Split('-')[0].ToString().ToUpper();
            //var html = "<article><address></address><table class=\"meta\"><tr><th><span>Receipt  #</span></th><td><span contenteditable></span></td> </tr><tr><th><span>Date</span></th><td><span contenteditable>#BookingDate</span></td></tr><tr></tr></table><table class=\"inventory\"><tr><th><span>PROJECT :</span></th><td><span>#Project</span></td><th><span>TOWER :</span></th><td><span>#Tower</span></td></tr><tr><th><span>NAME:</span></th><td><span>#CName</span></td><th><span>MOBILE :</span></th><td><span>#CMobile</span></td></tr><tr><th><span>AMOUNT PAID:</span></th><td><span>#BookingAmount</span></td><th><span>FLAT/PLOT NO :</span></th><td><span>#Flat</span></td></tr><tr><th><span>SFT:</span></th><td><span contenteditable>#SFT</span></td><th><span>MODE :</span></th><td><span>#Cheque</span></td></tr><tr><th><span>Ref No:</span></th><td><span>#RefNo</span></td></tr></table></article>";
            var html = "<table id='tblBookingDetails' class=\"table table-condensed\"><tr><th>Name</th><td>#CName</td><th>Contact</th><td>#Mobile</td></tr><tr><th>Aadhar</th><td>#Aadhar</td><th>Pan No</th><td>#PanNo</td></tr><tr><th>Project</th><td>#Project</td><th>Scheme</th><td>#SchemePercentage %</td></tr><tr><th>Flat/Plot No</th><td>#Flat</td><th>Sft Rate</th><td>#SftRate</td></tr><tr><th>Sft</th><td>#SFT</td><th>Bhk</th><td>#BHK</td></tr><tr><th>High Rise</th><td>#HIGHRISE</td><th>Total Rate</th><td><b>Rs.</b> #totalRate</td></tr> <tr><th>Discount</th><td>#discount</td><th>Final Rate</th><td><b>Rs.</b> #finalRate</td></tr><tr><th>ClubHouse</th><td><b>Rs.</b> #ClubHouse</td><th>Oth Charges</th><td><b>Rs.</b> #OtherCharges</td></tr><tr><th>Total</th><td><b>Rs.</b> #GrandRate</td><th>Scheme Due</th><td><b>Rs.</b> #SchemeDue</td></tr><tr><th>IBO Name</th><td>#AgentName</td><th>Booking Date</th><td>#BookingDate</td></tr><tr><th>Remarks</th><td colspan='3'>#Remarks</td></tr><tr><th>IBO Share</th><td><b>Rs.</b> #IBOShare</td><th>Comp Share</th><td><b>Rs.</b> #Company</td></tr></table>";
            html = html.Replace("#CName", result.Name)
                .Replace("#Project", result.ProjectName)
                .Replace("#Tower", result.TowerName)
                .Replace("#Aadhar", result.Aadhar == null ? "" : result.Aadhar.ToString())
                .Replace("#PanNo", result.Pan == null ? "" : result.Pan.ToString())
                .Replace("#Mobile", result.Mobile == null ? "" : result.Mobile.ToString())
                .Replace("#BookingDate", result.ChequeDate != null ? Convert.ToDateTime(result.ChequeDate).ToString("dd/MM/yyyy") : "")
                .Replace("#Flat", result.TowerName + "-" + result.FlatName)
                .Replace("#SFT", result.Area.ToString())
                .Replace("#SchemePercentage", result.SchemePercentage.ToString())
                .Replace("#Address", result.Address == null ? "" : result.Address.ToString())
                .Replace("#Cheque", result.PaymentMode.ToString())
                .Replace("#RefNo", result.ReferenceNo)
                .Replace("#BHK", result.Bhk.ToString())
                .Replace("#GrandRate", string.Format(inr, "{0:#,#}", result.GrandRate))
                .Replace("#finalRate", string.Format(inr, "{0:#,#}", result.FinalRate))
                .Replace("#discount", result.Discount.ToString())
                .Replace("#Remarks", result.Remarks)
                .Replace("#HIGHRISE", result.HighRiseCharges.ToString())
                .Replace("#totalRate", result.TotalRate.ToString())
                //.Replace("#Date", sysDate)
                //.Replace("#amountInWords", amountInWords)
                //.Replace("#bookingID", bookID)
                .Replace("#Company", result.CompanyShare.ToString() != "0" ? result.CompanyShare.ToString("#,#", new CultureInfo(0x0439)) : "0")
                .Replace("#IBOShare", result.IBOShare.ToString() != "0" ? result.IBOShare.ToString("#,#", new CultureInfo(0x0439)) : "0")
                .Replace("#SchemeDue", result.DueAmount.ToString() != "0" ? result.DueAmount.ToString("#,#", new CultureInfo(0x0439)) : "0")
                //.Replace("#GrandRate", "<b>Rs.</b>" + result.GrandRate.ToString())
                .Replace("#SftRate", result.SftRate.ToString())
                .Replace("#OtherCharges", result.OtherCharges.ToString() != "0" ? string.Format(inr, "{0:#,#}", result.OtherCharges) : "0")
                .Replace("#ClubHouse", result.ClubHouseCharges.ToString() != "0" ? string.Format(inr, "{0:#,#}", result.ClubHouseCharges) : "0")
                .Replace("#AgentName", result.AgentName);
            return Json(html, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProjectGallery()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }

        [HttpPost]
        public ActionResult ProjectGallery(ConstructionPic cv)
        {
            if (!cv.isVideo)
            {
                string FileName = Path.GetFileNameWithoutExtension(cv.File.FileName);

                //To Get File Extension  
                string FileExtension = Path.GetExtension(cv.File.FileName);

                //Add Current Date To Attached File Name  
                FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;
                if (!Directory.Exists(Server.MapPath("~/Content/Images/Construction")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Images/Construction"));
                }
                string path = "~/Content/Images/Construction/";
                string UploadPath = Server.MapPath(path);
                //Get Upload path from Web.Config file AppSettings.  
                //string UploadPath = "~/Content/Images/";

                //Its Create complete path to store in server.  
                string savePath = UploadPath + FileName;

                //To copy and save file into server.  
                cv.File.SaveAs(savePath);
                cv.URL = path.Remove(0, 1) + FileName;
            }
            cv.CreatedBy = User.Identity.Name;
            cv.CreatedDate = DateTime.Now.Date;
            cv.Active = true;
            var result = booking.SaveProjectGallery(cv);
            ModelState.Clear();
            if (result)
            {
                TempData["successmessage"] = "File Uploaded Successfully";
            }
            else
            {
                TempData["successmessage"] = "File Upload Failed";
            }

            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }

        [Authorize(Roles = "Admin,Customer")]
        public ActionResult DisplayProjectGallery()
        {
            List<ConstructionPic> lstProjectGallery = new List<ConstructionPic>();
            if (User.IsInRole("Admin"))
                lstProjectGallery = booking.GetProjectGallery("");
            else
                lstProjectGallery = booking.GetProjectGallery(User.Identity.Name);
            ViewBag.PicGallery = lstProjectGallery.Where(x => x.isVideo == false).ToList();
            var videoGallery = lstProjectGallery.Where(x => x.isVideo == true).ToList();
            foreach (var video in videoGallery)
            {
                var videoID = Helper.GetYouTubeId(video.URL);
                video.videoID = "https://img.youtube.com/vi/" + videoID + "/0.jpg";
            }
            ViewBag.VideoGallery = videoGallery;
            return View();
        }


        public ActionResult AddNews()
        {
            return View();
        }

        public JsonResult AddNewsDetails(string news, string newsDate)
        {
            NewsDetails nd = new NewsDetails();
            nd.News = news;
            nd.NewsDate = newsDate;
            nd.CreatedBy = User.Identity.Name;
            nd.CreatedDate = DateTime.Now.Date.ToString();
            var result = booking.SaveNews(nd);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNews()
        {
            var result = booking.GetNewsUpdates();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateNews(string models)
        {
            List<NewsDetails> data = JsonConvert.DeserializeObject<List<NewsDetails>>(models);
            var result = booking.UpdateNews(data[0]);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ApprovePaymentReceipt()
        {
            return View();
        }

        public JsonResult GetPaymentReceiptsForApproval()
        {
            var result = booking.GetPaymentReceiptsForApproval();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePaymentReceiptsForApproval(string models)
        {
            List<GetPaymentReceiptApproval> data = JsonConvert.DeserializeObject<List<GetPaymentReceiptApproval>>(models);
            
            var result = booking.UpdatePaymentReceiptsForApproval(data[0]);
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public ActionResult IBOAdvance()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            List<AgentMaster> agentList = booking.BindAgents();
            TempData["AgentList"] = new SelectList(agentList, "AgentID", "AgentName");
            TempData.Keep("ProjectList");
            TempData.Keep("AgentList");
            return View();
        }

        [HttpPost]
        public ActionResult IBOAdvance(IBOAdvanceForm advanceForm)
        {

            advanceForm.CreatedBy = User.Identity.Name;
            advanceForm.CreatedDate = DateTime.Now;

            var result = booking.ADDIBOAdvance(advanceForm);
            if (result)
            {
                TempData["successmessage"] = "IBO Advance Added Successfully";

            }
            else
            {
                TempData["successmessage"] = "IBO Advance Adding Failed, the flat selected in invalid for the selected IBO";
            }
            ModelState.Clear();
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            List<AgentMaster> agentList = booking.BindAgents();
            TempData["AgentList"] = new SelectList(agentList, "AgentID", "AgentName");
            TempData.Keep("ProjectList");
            TempData.Keep("AgentList");
            return View();
        }

        public JsonResult UpdateIBOAdvances(string models)
        {

            List<IBOAdvanceForm> data = JsonConvert.DeserializeObject<List<IBOAdvanceForm>>(models);
            var result = booking.UpdateIBOAdvances(data[0]);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIBOAdvances()
        {
            var result = booking.GetIBOAdvances();
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Cheques()
        {
            var list = common.BindChequeStatus();
            TempData["ChequeStatus"] = new SelectList(list, "Status", "Status");
            List<AgentMaster> agentList = booking.BindAgents();
            TempData["AgentList"] = new SelectList(agentList, "AgentID", "AgentName");
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult Cheques(Cheques cq)
        {
            //List<Cheque> data = JsonConvert.DeserializeObject<List<Cheque>>(cq);
            CultureInfo provider = CultureInfo.InvariantCulture;
            // It throws Argument null exception  
            cq.ChequeDate = DateTime.ParseExact(cq.ChequeDateString, "dd/MM/yyyy", provider);
            cq.CreatedBy = User.Identity.Name;
            cq.CreatedDate = DateTime.Now;
            var result = booking.AddCheque(cq);
            ModelState.Clear();
            if (result)
            {
                TempData["successmessage"] = "Cheque Added Successfully";

            }
            else
            {
                TempData["successmessage"] = "Cheque Adding Failed";
            }
            var list = common.BindChequeStatus();
            TempData["ChequeStatus"] = new SelectList(list, "Status", "Status");
            List<AgentMaster> agentList = booking.BindAgents();
            TempData["AgentList"] = new SelectList(agentList, "AgentID", "AgentName");
            TempData.Keep();
            return View();
        }

        public JsonResult GetCheques()
        {
            var result = booking.GetCheques();
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult UpdateCheque(string models)
        {
            List<Cheques> data = JsonConvert.DeserializeObject<List<Cheques>>(models);
            data[0].ModifiedBy = User.Identity.Name;
            data[0].ModifiedDate = DateTime.Now;
            var result = booking.UpdateCheque(data[0]);
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}