using BusinessLayer;
using DataLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using RestSharp;
using LoginApp.Models;

namespace LoginApp.Controllers
{
    [Authorize]
    public class FranchiseController : Controller
    {
        CommonBL common = new CommonBL();
        CommonDL commonDL = new CommonDL();
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
        // GET: Franchise
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {

            var agent = common.BindAgentsOnUsername(User.Identity.Name);
            if (agent.Count > 0)
            {
                ViewBag.AgentID = agent[0].AgentID;
                ViewBag.AgentName = agent[0].AgentName;
            }
            List<Country> countryList = common.BindCountry();
            TempData["CountryList"] = new SelectList(countryList, "CountryID", "CountryName");
            ViewBag.Result = null;
            TempData.Keep("CountryList");
            return View();

        }

        [HttpPost]
        public ActionResult Register(FranchiseRegistration fr)
        {


            

            //var path = System.AppContext.BaseDirectory + @"Content\Franchise\"+ fr.Receipt.FileName;
            try
            {
                if (!booking.CheckFranchiseRegistered(User.Identity.Name))
                {
                    Random generator = new Random();
                    int r = generator.Next(0, 1000000);
                    fr.RegisterNo = r;
                    var path = Server.MapPath("~/Franchise/");

                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    string extension = Path.GetExtension(fr.Receipt.FileName);
                    string path1 = path + r + extension;
                    fr.Receipt.SaveAs(path1);
                    fr.ReceiptPath = path1;
                    fr.CreatedBy = User.Identity.Name;
                    fr.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    fr.Status = "Submitted";
                    fr.Comments = "";
                    fr.Amount = 500000;
                    fr.Duration = "5";

                    var result = booking.RegisterFranchise(fr);
                    if (result)
                    {
                        ViewBag.Result = true;
                        ViewBag.Message = "Your Franchise has been successfully Registered with Reg no : " + r + " .Please check the status of approval with the same reg no.";
                        try
                        {
                            MailMessage msg = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            msg.From = new MailAddress("Info@sasinfra.in");
                            msg.To.Add(new MailAddress(User.Identity.Name));
                            msg.To.Add(new MailAddress("nsrinivas78@gmail.com"));
                            msg.To.Add(new MailAddress("manojvenkat8@gmail.com"));
                            //msg.To.Add(new MailAddress("Info@sasinfra.in"));
                            msg.Subject = "SAS : Franchise Registration";
                            //Attachment attachment = new Attachment();
                            //attachment.
                            if (fr.Receipt != null)
                            {
                                msg.Attachments.Add(new Attachment(fr.Receipt.InputStream, fr.Receipt.FileName));
                            }
                            msg.IsBodyHtml = true; //to make message body as html  
                            msg.Body = "<html><b>Your Franchise has been successfully Registered with Reg no : " + r + " <b><br/>" +
                                "<b>You can check the status of your franchise approval using the link : <a href='https://sasinfra.in/Franchise/AgreementStatus?id=" + r + "'>CHECK STATUS</a></b></html> ";
                            smtp.Port = 587;
                            smtp.Host = "mail.privateemail.com"; //for gmail host  
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential("Info@sasinfra.in", "sasinfo@123");
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(msg);
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Message = ex.Message;
                        }
                    }
                    else
                    {
                        ViewBag.Result = false;
                        ViewBag.Message = "Franchise Registration failed";
                    }
                }
                else
                {
                    ViewBag.Result = false;
                    ViewBag.Message = "There is already a franchise registered for your login. You cannot register for multiple franchises";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();

        }

        [AllowAnonymous]
        public ActionResult AgreementStatus(int id = 0)
        {
            ViewBag.RegNo = id;
            return View();
        }

        [AllowAnonymous]
        public ActionResult GetAgreementStatus(int id = 0)
        {
            var lst = booking.GetFranchiseStatus(id);
            ViewBag.StatusList = JsonConvert.SerializeObject(lst);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetAgreements()
        {

            return View();
        }

        public JsonResult GetFranchiseAgreements()
        {
            var franchise = booking.GetFranchiseAgreements();
            return Json(franchise, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Approval(int RegNo)
        {
            FranchiseRegistration fr = booking.GetFranchiseAgreements(RegNo);
            List<Country> countryList = common.BindCountry();
            TempData["CountryList"] = new SelectList(countryList, "CountryID", "CountryName");
            string path = "https://sasinfra.in/Franchise/DownloadReceipt?path=" + fr.ReceiptPath;
            ViewBag.DownloadURL = path;
            return View(fr);
        }

        [HttpPost]
        public ActionResult Approval(FranchiseRegistration fr)
        {
            fr.CreatedBy = User.Identity.Name;
            fr.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            //fr.Status = fr.ChangedStatus;

            var result = booking.UpdateFranchiseAgreements(fr);
            if (result && fr.Status == "MD Approved")
            {
                var franchiseOwnerEmail = booking.GetFranchiseOwnerEmail(fr);
                //var roleadd =  UserManager.AddToRolesAsync(franchiseOwnerEmail, "Franchise Owner");
            }
            List<Country> countryList = common.BindCountry();
            TempData["CountryList"] = new SelectList(countryList, "CountryID", "CountryName");
            //FranchiseRegistration frUpdated = booking.GetFranchiseAgreements(fr.RegisterNo);
            string path = "https://sasinfra.in/Franchise/DownloadReceipt?path=" + fr.ReceiptPath;
            ViewBag.DownloadURL = path;
            ViewBag.Message = "Updated Successfully";
            return RedirectToAction("GetAgreements", "Franchise");
        }

        public ActionResult DownloadReceipt(string path)
        {
            //string fullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\Templates\BulkUploadTemplate.csv");
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = Path.GetFileName(path);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult FranchiseBookingApproval()
        {
            return View();
        }

        public JsonResult GetFranchiseBookings()
        {
            var franchise = booking.GetFranchiseBookings();
            return Json(franchise, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> UpdateFranchiseBookings(string models)
        {
            List<FranchiseBookings> data = JsonConvert.DeserializeObject<List<FranchiseBookings>>(models);
            var result = booking.UpdateFranchiseBookings(data[0]);
            if(result)
            {
                if(data[0].Status == "Approved")
                {
                    var customer = booking.GetCustomerInfo(data[0].BookingID);

                    //CreateCustomerLogin(customer);
                    var user = new ApplicationUser { UserName = customer.Email, Email = customer.Email, PhoneNumber = customer.Mobile.ToString() };
                    var result1 = await UserManager.CreateAsync(user, "Welcome@123");
                    var roleadd = await UserManager.AddToRoleAsync(user.Id, "Customer");
                    var message = "Welcome to Sri Aditya Squares" + Environment.NewLine + "Below are your login credetials:" + Environment.NewLine + "Username :" + customer.Email + Environment.NewLine + "Password :" + "Welcome@123" + Environment.NewLine + "Please use the link to login to the application :" + "https://sasinfra.in";
                    var client = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + customer.Mobile + "&smsContentType=english");
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
                    sms.Recipients = customer.Mobile + "," + "8121751751";
                    sms.CreatedBy = data[0].CreatedBy;
                    TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                    //var sysDate = Convert.ToDateTime(indianTime).ToString("dd/MM/yyyy");
                    sms.CreatedDate = indianTime;
                    CommonDL common = new CommonDL();
                    common.LogSMS(sms);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> CreateCustomerLogin(CustomerInfo customer)
        {
            
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}