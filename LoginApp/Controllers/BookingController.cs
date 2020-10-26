using BusinessLayer;
using DataLayer;
using log4net;
using LoginApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ModelLayer;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
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

        [Authorize(Roles = "Admin,DataEntry")]
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
                    if(b.PaymentModeID == "1")
                        b.ChequeStatus = "Received";
                    else
                        b.ChequeStatus = "Not-Applicable";
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
                    //var user = new ApplicationUser { UserName = b.Email, Email = b.Email };
                    if (isNew)
                    {

                        var user = new ApplicationUser { UserName = b.Email, Email = b.Email, PhoneNumber = b.Mobile.ToString() };
                        var result1 = await UserManager.CreateAsync(user, "Welcome@123");

                        if (result1.Succeeded)
                        {
                            var roleadd = await UserManager.AddToRoleAsync(user.Id, "Customer");

                            var message = "Welcome to Sri Aditya Squares" + Environment.NewLine + "Below are your login credetials:" + Environment.NewLine + "Username :" + b.Email + Environment.NewLine + "Password :" + "Welcome@123" + Environment.NewLine + "Please use the link to login to the application :" + "https://sasinfra.in";
                            var client = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + b.Mobile + "&smsContentType=english");
                            var request = new RestRequest(Method.GET);
                            request.AddHeader("Cache-Control", "no-cache");
                            IRestResponse response = client.Execute(request);
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
            if (!User.IsInRole("Customer"))
            {
                towerList = booking.BindTowers(ProjectId);
            }
            else
                towerList = booking.BindCustomerTowers(User.Identity.Name);
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


        [Authorize(Roles = "Admin,DataEntry")]
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
                if (Request.Form["GenerateReceipt"] == null)
                {
                    //payInfo.CreatedDate = DateTime.ParseExact(payInfo.PaymentDate, "dd/MM/yyyy", null);
                    payInfo.CreatedBy = User.Identity.Name;
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
            List<Flats> flatList = new List<Flats>();
            if (User.IsInRole("Customer"))
            {
                flatList = booking.BindCustomerFlats(User.Identity.Name);
            }
            else
                flatList = booking.BindFlatsExceptOpen(TowerId);
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
                    html = html + "<div class=\"col-sm-4\"><div class=\"card bg-info text-white\"><img  src = \"" + customerVisits[i + j].SelfieURL + "\" style =\"width:100%;height:400px;object-fit: contain;\" /><div class=\"containercard\"><h4><b>" + customerVisits[i + j].AgentName + "</b></h4><h6><b>CUSTOMER :" + customerVisits[i + j].CustomerName + "</b></h6><h6><b>PROJECT  :" + customerVisits[i + j].ProjectName + "</b></h6><h6><b>DATE     : " + customerVisits[i + j].DateAdded.ToString() + "</b></h6></div></div></div>";

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
            var result = booking.AddSiteVisit(siteVisitInfo);
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
            var result = booking.UpdateSiteVisitApproval(data[0]);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [Authorize(Roles = "Admin,DataEntry")]
        public ActionResult Invoice()
        {
            List<Projects> projectList = booking.BindProjects();
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
            var bookingDate = Convert.ToDateTime(result.CreatedDate).ToString("dd/MM/yyyy");
            var amountInWords = AmountInWords(Convert.ToDouble(result.BookingAmount));
            var bookID = result.BookingID.ToString().Split('-')[0].ToString().ToUpper();
            //var html = "<article><address></address><table class=\"meta\"><tr><th><span>Receipt  #</span></th><td><span contenteditable></span></td> </tr><tr><th><span>Date</span></th><td><span contenteditable>#BookingDate</span></td></tr><tr></tr></table><table class=\"inventory\"><tr><th><span>PROJECT :</span></th><td><span>#Project</span></td><th><span>TOWER :</span></th><td><span>#Tower</span></td></tr><tr><th><span>NAME:</span></th><td><span>#CName</span></td><th><span>MOBILE :</span></th><td><span>#CMobile</span></td></tr><tr><th><span>AMOUNT PAID:</span></th><td><span>#BookingAmount</span></td><th><span>FLAT/PLOT NO :</span></th><td><span>#Flat</span></td></tr><tr><th><span>SFT:</span></th><td><span contenteditable>#SFT</span></td><th><span>MODE :</span></th><td><span>#Cheque</span></td></tr><tr><th><span>Ref No:</span></th><td><span>#RefNo</span></td></tr></table></article>";
            var html = "<article><address></address><table class=\"meta\"><tr><th><span>Receipt No</span></th><td><span contenteditable>#bookingID</span></td> </tr><tr><th><span>Date</span></th><td><span contenteditable>#Date</span></td></tr><tr></tr></table><table class=\"inventory\"><tr><th>Project</th><td>#Project</td><th>Tower</ th ><td>#Tower</td></tr><tr><th>Name</th><td colspan = '3'>#CName</td></tr><tr><th>Mobile</th><td contenteditable>#CMobile</td><th>Amount Paid</th><td>#BookingAmount</td></tr><tr><th>Flat/Plot No</th><td>#Flat</td><th>Booking Date</th><td contenteditable>#BookingDate</td></tr><tr><th >Sft</th><td>#SFT</td><th>Bhk</th><td>#BHK</td></tr><tr><th>Mode</th><td contenteditable>#Cheque</td><th>Ref No</th><td contenteditable >#RefNo</td></tr> <tr><th> Amount in Words </th><td contenteditable colspan = '3' >#amountInWords</td></tr><tr><th> Bank Name </th><td contenteditable colspan = '3' > &nbsp;</td> </tr></table></article>";
            html = html.Replace("#BookingDate", bookingDate).Replace("#Project", result.ProjectName).Replace("#Tower", result.TowerName).Replace("#CName", result.Name).Replace("#CMobile", result.Mobile == null ? "" : result.Mobile.ToString()).Replace("#BookingAmount", "Rs. " + result.BookingAmount.ToString()).Replace("#Flat", result.FlatName).Replace("#SFT", result.Area.ToString()).Replace("#Cheque", result.PaymentMode.ToString()).Replace("#RefNo", result.ReferenceNo).Replace("#BHK", result.Bhk.ToString()).Replace("#Date", sysDate).Replace("#amountInWords", amountInWords).Replace("#bookingID", bookID);
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,DataEntry")]
        public ActionResult PaymentReceipts()
        {
            List<Projects> projectList = booking.BindProjects();
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
            var payDate = Convert.ToDateTime(result.PaymentDate).ToString("dd/MM/yyyy");
            var amountInWords = AmountInWords(Convert.ToDouble(result.BookingAmount));
            var html = "<article><address></address><table class=\"meta\"><tr><th> Pay ID:#</th><td><span contenteditable>#PaymentID</span></td></tr><tr><th>Date</th><td><span contenteditable>#Date</span></td></tr></tbody></table><table class=\"inventory\"><tbody><tr><th>Project</th><td>#Project</td><th>Tower</th><td>#Tower</td></tr><tr><th>Name</th><td colspan = '3'>#CName</td></tr><th>Mobile</th><td contenteditable>#CMobile</td><th>Amount Paid</th><td>#BookingAmount</td></tr><tr><th>Flat/Plot No</th><td>#Flat</td><th>Payment Date</th><td contenteditable>#PaymentDate</td></tr><tr><th >Sft</th><td>#SFT</td><th>Bhk</th><td>#BHK</td></tr><tr><th>Mode</th><td contenteditable>#Cheque</td><th>Ref No</th><td><span contenteditable>#RefNo</span></td></tr><tr><th>Amount in Words</th><td colspan = '3' contenteditable>#amountInWords</td></tr><tr><th>Bank Name</th><td colspan = '3' contenteditable></td></tr></table></article>";
            html = html.Replace("#Project", result.ProjectName).Replace("#Tower", result.TowerName).Replace("#CName", result.Name).Replace("#CMobile", result.Mobile == null ? "" : result.Mobile.ToString()).Replace("#BookingAmount", "Rs. " + result.BookingAmount.ToString()).Replace("#Flat", result.FlatName).Replace("#SFT", result.Area.ToString()).Replace("#Cheque", result.PaymentMode.ToString()).Replace("#RefNo", result.ReferenceNo).Replace("#PaymentID", result.PaymentID.ToString()).Replace("#PaymentDate", payDate).Replace("#Date", sysDate).Replace("#amountInWords", amountInWords).Replace("#BHK", result.Bhk.ToString());
            return Json(html, JsonRequestBehavior.AllowGet);
        }


        public string AmountInWords(double? numbers, Boolean paisaconversion = false)
        {
            var pointindex = numbers.ToString().IndexOf(".");
            var paisaamt = 0;
            if (pointindex > 0)
                paisaamt = Convert.ToInt32(numbers.ToString().Substring(pointindex + 1, 2));

            int number = Convert.ToInt32(numbers);

            if (number == 0) return "Zero";
            if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }

            if (paisaamt == 0 && paisaconversion == false)
            {
                sb.Append("rupees only");
            }
            else if (paisaamt > 0)
            {
                var paisatext = AmountInWords(paisaamt, true);
                sb.AppendFormat("rupees {0} paise only", paisatext);
            }
            return sb.ToString().TrimEnd();
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

            return View();
        }

        [HttpPost]
        public ActionResult DailyExpenses(DailyExpense dailyExpense)
        {

            dailyExpense.CreatedBy = User.Identity.Name;
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            dailyExpense.CreatedDate = Convert.ToDateTime(indianTime).ToString("dd/MM/yyyy");
            var result = booking.AddDailyExpense(dailyExpense);
            if (result)
            {
                TempData["successmessage"] = "Expense Added Successfully";

            }
            else
            {
                TempData["successmessage"] = "Expense Adding Failed";
            }
            ModelState.Clear();
            return View();
        }

        public JsonResult GetDailyExpenses()
        {
            var result = booking.GetDailyExpenses();
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
    }


}