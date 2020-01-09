using BusinessLayer;
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
            agentsList = booking.BindAgents();
            foreach (var item in agentsList)
            {
                //item.AgentParentArray =  (item.AgentParent ?? "").Split(',').Select<string, int>(int.Parse);
                //int[] arrayListParent;
                //if (!String.IsNullOrEmpty(item.AgentParent))
                //{
                //    item.arrayListParent = Array.ConvertAll(item.AgentParent.Split(','), int.Parse);
                //}
                //else
                //{
                //    item.arrayListParent = null;
                //}
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
                //if(result)
                //{
                //    TempData["successmessage"] = "Update Successfull";
                //}
                //else
                //{
                //    TempData["successmessage"] = "Update Successfull";
                //}
                // TODO: Add update logic here

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
