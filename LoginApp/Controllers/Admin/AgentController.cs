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
    [Authorize(Roles = "Admin")]
    public class AgentController : Controller
    {
        private static readonly ILog log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<AgentMaster> agentsList = new List<AgentMaster>();
        BookingBL booking = new BookingBL();
        AgentBL agent = new AgentBL();
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
            catch(Exception ex)
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
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<AgentMaster> data = JsonConvert.DeserializeObject<List<AgentMaster>>(models,settings);
                data[0].CreatedBy = User.Identity.Name;
                data[0].CreatedDate = DateTime.Now.Date;
                var result = agent.AddAgent(data[0]);
                if(result)
                {
                    var user = new ApplicationUser { UserName = data[0].AgenteMail, Email = data[0].AgenteMail, PhoneNumber = data[0].AgentMobileNo.ToString() };
                    var result1 = await UserManager.CreateAsync(user, "Welcome@123");
                    
                    if(result1.Succeeded)
                    {
                        var roleadd = await UserManager.AddToRoleAsync(user.Id, "Agent");
                    }
                    var message = "Username :" + data[0].AgenteMail + Environment.NewLine + "Password :" + "Welcome@123";
                    var client = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + data[0].AgentMobileNo + "&smsContentType=english");
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Cache-Control", "no-cache");
                    IRestResponse response = client.Execute(request);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
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
                List<AgentMaster> data = JsonConvert.DeserializeObject<List<AgentMaster>>(models);
                data[0].UpdatedBy = User.Identity.Name;
                data[0].UpdatedDate = DateTime.Now.Date;
                var result = agent.UpdateAgent(data[0]);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
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

       
        
    }
}
