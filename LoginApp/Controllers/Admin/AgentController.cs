using BusinessLayer;
using log4net;
using LoginApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ModelLayer;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers.Admin
{
    [Authorize(Roles = "Admin,Franchise Owner,DataEntry")]
    public class AgentController : Controller
    {
        private static readonly ILog log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<AgentMaster> agentsList = new List<AgentMaster>();
        BookingBL booking = new BookingBL();
        AgentBL agent = new AgentBL();
        CommonBL common = new CommonBL();
        // GET: Project
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

        [Authorize(Roles = "Admin,DataEntry")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Project/Details/5
        public ActionResult Details()
        {
            try
            {
                agentsList = booking.BindAgents();
                ViewData["Exception"] = "Empty";
                foreach (var item in agentsList)
                {
                    switch (item.AgentStatus)
                    {
                        case "A":
                            item.BookingStatusName = "ACTIVE";
                            break;
                        case "I":
                            item.BookingStatusName = "INACTIVE";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(agentsList, JsonRequestBehavior.AllowGet);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        [HttpGet]
        public async Task<ActionResult> CreateAgent(string models)
        {
            try
            {
                ViewData["Exception"] = "Empty";
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<AgentMaster> data = JsonConvert.DeserializeObject<List<AgentMaster>>(models, settings);
                if (!String.IsNullOrEmpty(data[0].AgenteMail))
                {
                    data[0].CreatedBy = User.Identity.Name;
                    data[0].CreatedDate = DateTime.Now;
                    if (User.IsInRole("Franchise Owner"))
                    {
                        data[0].AgentStatus = "I";
                        data[0].FranchiseID = booking.GetFranchiseID(User.Identity.Name);
                    }
                    else
                        if (User.IsInRole("Agent"))
                    {
                        data[0].AgentStatus = "I";
                    }
                    var result = agent.AddAgent(data[0]);
                    if (result)
                    {
                        var user = new ApplicationUser { UserName = data[0].AgenteMail, Email = data[0].AgenteMail, PhoneNumber = data[0].AgentMobileNo.ToString() };
                        var result1 = await UserManager.CreateAsync(user, "Welcome@123");

                        if (result1.Succeeded)
                        {
                            var roleadd = await UserManager.AddToRoleAsync(user.Id, "Agent");

                            var message = "Welcome to Sri Aditya Squares" + Environment.NewLine + "Below are your login credetials:" + Environment.NewLine + "Username :" + data[0].AgenteMail + Environment.NewLine + "Password :" + "Welcome@123" + Environment.NewLine + "Agent Code :" + data[0].AgentCode + Environment.NewLine + "Please use the link to login to the application :" + "https://sasinfra.in";
                            //var message = "Hi";
                            var client = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + data[0].AgentMobileNo + "&smsContentType=english");
                            var request = new RestRequest(Method.GET);
                            request.AddHeader("Cache-Control", "no-cache");
                            IRestResponse response = client.Execute(request);

                            var client1 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + 8121751751 + "&smsContentType=english");
                            var request1 = new RestRequest(Method.GET);
                            request1.AddHeader("Cache-Control", "no-cache");
                            IRestResponse response1 = client1.Execute(request1);

                            SMS sms = new SMS();
                            sms.MessageType = "Agent Account Creation";
                            sms.Message = message;
                            sms.Recipients = data[0].AgentMobileNo + "," + "8121751751";
                            sms.CreatedBy = User.Identity.Name;
                            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                            var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                            //var sysDate = Convert.ToDateTime(indianTime).ToString("dd/MM/yyyy");
                            sms.CreatedDate = indianTime;
                            CommonBL common = new CommonBL();
                            common.LogSMS(sms);
                        }
                        else
                        {
                            result = false;
                        }

                    }
                    else
                    {
                        if (data[0].isDuplicateAgentCode)
                        {
                            ViewData["Exception"] = "Duplicate Agent Code";
                            Convert.ToInt32("Duplicate Agent Code");
                        }

                        else
                            if (data[0].isDuplicateAgentEmail)
                        {
                            ViewData["Exception"] = "Duplicate Agent Email";
                            Convert.ToInt32("Duplicate Agent Email");
                        }


                        else
                            if (data[0].isDuplicateAgentMobile)
                        {
                            ViewData["Exception"] = "Duplicate Agent Mobile";
                            Convert.ToInt32("Duplicate Agent Mobile");
                        }

                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ViewData["Exception"] = "Agent Email cannot be empty";
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                    
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        // POST: Project/Edit/5
        [HttpGet]
        public ActionResult UpdateAgent(string models)
        {
            try
            {

                ViewData["Exception"] = "Empty";
                List<AgentMaster> data = JsonConvert.DeserializeObject<List<AgentMaster>>(models);
                if (User.IsInRole("Franchise Owner"))
                {
                    data[0].AgentStatus = "I";
                    data[0].FranchiseID = booking.GetFranchiseID(User.Identity.Name);
                }
                else
                    if (User.IsInRole("Agent"))
                {
                    data[0].AgentStatus = "I";
                }
                data[0].UpdatedBy = User.Identity.Name;
                data[0].UpdatedDate = DateTime.Now;

                var result = agent.UpdateAgent(data[0]);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        public ActionResult AgentMapping()
        {
            var agentList = common.BindAgents2();
            TempData["AgentList"] = new SelectList(agentList, "AgentID", "AgentName");
            return View();
        }

        public JsonResult GetMapping(int AgentID, int option)
        {
            var list = agent.GetAgentMapping(AgentID, option);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateMapping(int AgentID, int option, string agentList)
        {
            var result = agent.UpdateAgentMapping(AgentID, option, agentList, User.Identity.Name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Franchise Owner")]
        public ActionResult FranchiseAgents()
        {
            return View();
        }

        public ActionResult FranchiseAgentDetails()
        {
            var agentsList = new List<AgentMaster>();
            try
            {
                agentsList = booking.BindFranchiseAgents(User.Identity.Name);
                ViewData["Exception"] = "Empty";
                foreach (var item in agentsList)
                {
                    switch (item.AgentStatus)
                    {
                        case "A":
                            item.BookingStatusName = "ACTIVE";
                            break;
                        case "I":
                            item.BookingStatusName = "INACTIVE";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(agentsList, JsonRequestBehavior.AllowGet);
        }



        // POST: Project/Edit/5
        [HttpGet]
        public ActionResult UpdateFranchiseAgent(string models)
        {
            try
            {

                ViewData["Exception"] = "Empty";
                List<AgentMaster> data = JsonConvert.DeserializeObject<List<AgentMaster>>(models);
                data[0].UpdatedBy = User.Identity.Name;
                data[0].UpdatedDate = DateTime.Now;

                var result = agent.UpdateAgent(data[0]);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize(Roles = "Admin,Agent")]
        public ActionResult TeamAgents()
        {
            return View();
        }

        public ActionResult TeamAgentDetails()
        {
            var agentsList = new List<AgentMaster>();
            try
            {
                agentsList = booking.BindTeamAgents(User.Identity.Name);
                ViewData["Exception"] = "Empty";
                foreach (var item in agentsList)
                {
                    switch (item.AgentStatus)
                    {
                        case "A":
                            item.BookingStatusName = "ACTIVE";
                            break;
                        case "I":
                            item.BookingStatusName = "INACTIVE";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(agentsList, JsonRequestBehavior.AllowGet);
        }



        // POST: Project/Edit/5
        [HttpGet]
        public ActionResult TeamUpdateAgent(string models)
        {
            try
            {

                ViewData["Exception"] = "Empty";
                List<AgentMaster> data = JsonConvert.DeserializeObject<List<AgentMaster>>(models);
                data[0].UpdatedBy = User.Identity.Name;
                data[0].UpdatedDate = DateTime.Now;

                var result = agent.UpdateAgent(data[0]);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
