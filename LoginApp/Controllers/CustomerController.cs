using BusinessLayer;
using ModelLayer;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class CustomerController : Controller
    {
        BookingBL booking = new BookingBL();
        AdminBL admin = new AdminBL();
        // GET: Customer
        public ActionResult Enquiry()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }

        [HttpPost]
        public ActionResult Enquiry(CustomerEnquiry customerEnquiry)
        {
            if (customerEnquiry.Enquiry.Length < 100)
            {
                List<GetAgentLocations> agentLocations = booking.GetAgentLocations();
                GetAgentLocations agent = new GetAgentLocations();
                //double minDistance = 9999;
                string customerLocation = "https://www.google.com/maps/place/" + customerEnquiry.Latitude + "," + customerEnquiry.Longitude + "/@" + customerEnquiry.Latitude + ',' + customerEnquiry.Longitude;
                foreach (var item in agentLocations)
                {
                    if (item.AgentLatitude != null && item.AgentLongitude != null)
                    {
                        item.Distance = distance(customerEnquiry.Latitude, Convert.ToDouble(item.AgentLatitude), customerEnquiry.Longitude, Convert.ToDouble(item.AgentLongitude));
                    }
                    //22bd272c6ec1a7de6af16ae3818ab
                }
                TempData.Keep("ProjectList");
                var agents = agentLocations.Where(x => x.Distance != null).OrderBy(x => x.Distance).Take(5);
                var message = "";
                var agentNames = "";
                int i = 0;
                foreach (var currentAgent in agents)
                {
                    //currentAgent.AgentMobileNo = 9505055755;
                    //message = "Hello, we got a new enquiry from the below Customer :\n  Customer Name :" + customerEnquiry.CustomerName + Environment.NewLine + "Mobile no :" + customerEnquiry.Mobile + Environment.NewLine + "Location :" + customerLocation + Environment.NewLine + "Requirement :" + customerEnquiry.Enquiry;
                    //var client = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + currentAgent.AgentMobileNo + "&smsContentType=english");
                    //var request = new RestRequest(Method.GET);
                    //request.AddHeader("Cache-Control", "no-cache");
                    //IRestResponse response = client.Execute(request);
                    //if (i != 0)
                    //{
                    //    agentNames = agentNames + "," + currentAgent.AgentName;
                    //}
                    //else
                    //    agentNames = currentAgent.AgentName;

                    //i++;
                }
                message = "Hello, we got a new enquiry from the below Customer :\n  Customer Name :" + customerEnquiry.CustomerName + Environment.NewLine + "Mobile no :" + customerEnquiry.Mobile + Environment.NewLine + "Location :" + customerLocation + Environment.NewLine + "Requirement :" + customerEnquiry.Enquiry;
                var client1 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + 8121751751 + "&smsContentType=english");
                var request1 = new RestRequest(Method.GET);
                request1.AddHeader("Cache-Control", "no-cache");
                IRestResponse response1 = client1.Execute(request1);
                customerEnquiry.Sms = message;
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                customerEnquiry.EnquiryDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                customerEnquiry.Recipients = agentNames;
                booking.SaveCustomerInquiry(customerEnquiry);
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult DownloadBrochure(string fileName)
        {
            string fullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\Brochures\"+fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult DownloadLegalOpinion(string fileName)
        {
            string fullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\LegalOpinion\" + fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }


        public ActionResult UploadProjectImages()
        {
            List<Projects> projectList = booking.BindProjects();
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }

        public JsonResult GetProjectImages()
        {
            var result = admin.BindProjectGallery();
            foreach (var item in result)
            {
                item.URL = Path.GetFileName(item.URL);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateProjectImages(string models)
        {
            List<ProjectPics> data = JsonConvert.DeserializeObject<List<ProjectPics>>(models);
            data[0].CreatedBy = User.Identity.Name;
            data[0].CreatedDate = DateTime.Now;

            var result = admin.UpdateProjectImage(data[0]);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadProjectImages(ProjectPics cv)
        {
            string FileName = Path.GetFileNameWithoutExtension(cv.File.FileName);

            //To Get File Extension  
            string FileExtension = Path.GetExtension(cv.File.FileName);

            //Add Current Date To Attached File Name  
            FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;
            if (!Directory.Exists(Server.MapPath("~/Content/Images/Projects")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Content/Images/Projects"));
            }
            string path = "~/Content/Images/Projects/";
            string UploadPath = Server.MapPath(path);
            //Get Upload path from Web.Config file AppSettings.  
            //string UploadPath = "~/Content/Images/";

            //Its Create complete path to store in server.  
            string savePath = UploadPath + FileName;

            //To copy and save file into server.  
            cv.File.SaveAs(savePath);
            cv.URL = path.Remove(0, 1) + FileName;
            cv.CreatedBy = User.Identity.Name;
            cv.CreatedDate = DateTime.Now.Date;
            cv.Active = true;
            var result = admin.SaveProjectImages(cv);
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
        static double toRadians(
           double angleIn10thofaDegree)
        {
            // Angle in 10th 
            // of a degree 
            return (angleIn10thofaDegree *
                           Math.PI) / 180;
        }
        static double distance(double lat1,
                               double lat2,
                               double lon1,
                               double lon2)
        {

            // The math module contains  
            // a function named toRadians  
            // which converts from degrees  
            // to radians. 
            lon1 = toRadians(lon1);
            lon2 = toRadians(lon2);
            lat1 = toRadians(lat1);
            lat2 = toRadians(lat2);

            // Haversine formula  
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            // Radius of earth in  
            // kilometers. Use 3956  
            // for miles 
            double r = 6371;

            // calculate the result 
            return (c * r);
        }
    }
}