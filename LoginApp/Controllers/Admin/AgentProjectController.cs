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
    [Authorize(Roles = "Admin")]
    public class AgentProjectController : Controller
    {
        private static readonly ILog log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        List<AgentProjectLevel> list = new List<AgentProjectLevel>();

        BookingBL booking = new BookingBL();
        AdminBL agentLevels = new AdminBL();
        CommonBL common = new CommonBL();
        // GET: AgentProject
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Details()
        {
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            try
            {
                list = booking.BindAgentProjectLevels();
                jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);

            }

            return jsonResult;
        }

        [HttpGet]
        public ActionResult Create(string models)
        {
            bool result = false;
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<AgentProjectLevel> data = JsonConvert.DeserializeObject<List<AgentProjectLevel>>(models, settings);
                data[0].CreatedBy = User.Identity.Name;
                data[0].CreatedDate = System.DateTime.Now;
                 result = agentLevels.AddAgentProjectLevel(data[0]);
            }
            catch(Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Update(string models)
        {
            bool result = false;
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<AgentProjectLevel> data = JsonConvert.DeserializeObject<List<AgentProjectLevel>>(models, settings);
                data[0].UpdatedBy = User.Identity.Name;
                data[0].UpdatedDate = System.DateTime.Now;
                result = agentLevels.UpdateAgentProjectLevel(data[0]);
            }
            catch(Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}