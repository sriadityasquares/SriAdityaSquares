using BusinessLayer;
using ModelLayer;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class CustomerController : Controller
    {
        BookingBL booking = new BookingBL();
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
            List<GetAgentLocations> agentLocations = booking.GetAgentLocations();
            GetAgentLocations agent = new GetAgentLocations();
            //double minDistance = 9999;
            string customerLocation = "https://www.google.com/maps/place/"+customerEnquiry.Latitude+","+customerEnquiry.Longitude+"/@"+customerEnquiry.Latitude+','+customerEnquiry.Longitude;
            foreach(var item in agentLocations)
            {
                if(item.AgentLatitude != null && item.AgentLongitude !=null)
                {
                    item.Distance = distance(customerEnquiry.Latitude, Convert.ToDouble(item.AgentLatitude), customerEnquiry.Longitude, Convert.ToDouble(item.AgentLongitude));
                }
                //22bd272c6ec1a7de6af16ae3818ab
            }
            TempData.Keep("ProjectList");
            var agents =  agentLocations.Where(x=>x.Distance != null).OrderBy(x => x.Distance).Take(5);
            var message = "";
            var agentNames = "";
            int i = 0;
            foreach (var currentAgent in agents)
            {
                //currentAgent.AgentMobileNo = 9505055755;
                message = "Hello, we got a new enquiry from the below Customer :\n  Customer Name :" + customerEnquiry.CustomerName + Environment.NewLine + "Mobile no :" + customerEnquiry.Mobile + Environment.NewLine + "Location :" + customerLocation + Environment.NewLine + "Requirement :" + customerEnquiry.Enquiry;
                var client = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + currentAgent.AgentMobileNo + "&smsContentType=english");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Cache-Control", "no-cache");
                IRestResponse response = client.Execute(request);
                if(i != 0)
                {
                    agentNames = agentNames + "," + currentAgent.AgentName;
                }
                else
                    agentNames = currentAgent.AgentName;

                i++;
            }
            customerEnquiry.Sms = message;
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            customerEnquiry.EnquiryDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            customerEnquiry.Recipients = agentNames;
            booking.SaveCustomerInquiry(customerEnquiry);
            return RedirectToAction("Index", "Home");
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