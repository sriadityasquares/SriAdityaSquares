using BusinessLayer;
using log4net;
using ModelLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers.Admin
{
    public class AgentController : Controller
    {
        private static readonly ILog log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<AgentMaster> agentsList = new List<AgentMaster>();
        BookingBL booking = new BookingBL();
        AgentBL agent = new AgentBL();
        // GET: Project
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
        public ActionResult CreateAgent(string models)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<AgentMaster> data = JsonConvert.DeserializeObject<List<AgentMaster>>(models,settings);
                var result = agent.AddAgent(data[0]);
                

                return Json(true, JsonRequestBehavior.AllowGet);
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
