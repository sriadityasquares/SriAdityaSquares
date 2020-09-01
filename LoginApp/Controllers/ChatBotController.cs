using BusinessLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class ChatBotController : Controller
    {
        // GET: ChatBot
        BookingBL booking = new BookingBL();
        public JsonResult GetProjectsForChatBot()
        {
            List<Projects> projectList = booking.BindProjects();
            List<SuggestedActions> lstSuggestedActions = new List<SuggestedActions>();
            foreach (var p in projectList)
            {
                SuggestedActions suggestedActions = new SuggestedActions();
                suggestedActions.title = p.ProjectName;
                suggestedActions.value = "Project-" + p.ProjectName;
                lstSuggestedActions.Add(suggestedActions);
            }
            return Json(lstSuggestedActions, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetProjectLocationsForChatBot()
        {
            List<Projects> projectList = booking.BindProjects();
            var locations = projectList.Select(x => x.ProjectLocation).Distinct().ToList();
            List<SuggestedActions> lstSuggestedActions = new List<SuggestedActions>();
            for (int i = 0; i < locations.Count; i++)
            {
                SuggestedActions suggestedActions = new SuggestedActions();
                suggestedActions.title = locations[i];
                suggestedActions.value = "Location-" + locations[i];
                lstSuggestedActions.Add(suggestedActions);
            }
            return Json(lstSuggestedActions, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetProjectBasedOnLocationsForChatBot(string locationName)
        {
            List<Projects> projectList = booking.BindProjectsBasedOnLocation(locationName);
            List<SuggestedActions> lstSuggestedActions = new List<SuggestedActions>();
            foreach (var p in projectList)
            {
                SuggestedActions suggestedActions = new SuggestedActions();
                suggestedActions.title = p.ProjectName;
                suggestedActions.value = "Project-" + p.ProjectName;
                lstSuggestedActions.Add(suggestedActions);
            }
            return Json(lstSuggestedActions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProjectDetailsForChatBot(string projectName)
        {
            Projects project = booking.BindProjectDetails(projectName);
            project.projectImageURL = @"/Content/Images/" + projectName + ".png";
            return Json(project, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetProjectSuggestionForChatBot(string projectName)
        {
            List<SuggestedActions> lstSuggestedActions = new List<SuggestedActions>();
            SuggestedActions suggestedActions1 = new SuggestedActions();
            suggestedActions1.title = "Project Details";
            suggestedActions1.value = projectName + "-Details";
            lstSuggestedActions.Add(suggestedActions1);
            SuggestedActions suggestedActions2 = new SuggestedActions();
            suggestedActions2.title = "Approvals";
            suggestedActions2.value = projectName + "-Approvals";
            lstSuggestedActions.Add(suggestedActions2);
            SuggestedActions suggestedActions3 = new SuggestedActions();
            suggestedActions3.title = "Pricing";
            suggestedActions3.value = projectName + "-Pricing";
            lstSuggestedActions.Add(suggestedActions3);
            return Json(lstSuggestedActions, JsonRequestBehavior.AllowGet);
        }
    }
}