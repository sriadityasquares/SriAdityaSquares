using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
using ModelLayer;

namespace LoginApp.Controllers
{
    [Authorize]
    public class CascadeController : Controller
    {
        CommonBL objCasc = new CommonBL();
        BookingBL objBooking = new BookingBL();
        // GET: Cascade
        public ActionResult Index()
        {
            List<Country> countryList = objCasc.BindCountry();
            ViewBag.CountryList = new SelectList(countryList, "CountryID", "CountryName");
            ViewBag.ProjectList = new SelectList(countryList, "CountryID", "CountryName");
            ViewBag.AgentList = new SelectList(countryList, "CountryID", "CountryName");
            return View();
        }


        public JsonResult GetStateList(int CountryId)
        {
            List<State> StateList = objCasc.BindState(CountryId);
            return Json(StateList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetAreaList(int CityId)
        {
            List<Area> AreaList = objCasc.BindArea(CityId);
            return Json(AreaList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStatus()
        {
            List<Status> statusList = objCasc.BindStatus();
            return Json(statusList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetAgents()
        {
            List<Agent> agentList = objCasc.BindAgents();
            return Json(agentList, JsonRequestBehavior.AllowGet);

        }

       

        public JsonResult GetAgentsExceptSAS()
        {
            List<Agent> agentList = objCasc.BindAgentsExceptSAS();
            return Json(agentList, JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetAgents2()
        {
            List<AgentDropdown> agentList = new List<AgentDropdown>();
            if (!User.IsInRole("Franchise Owner"))
            {
                agentList = objCasc.BindAgents2();
            }
            else
            {
                agentList = objCasc.BindAgents2Franchise(User.Identity.Name);
            }
            
            return Json(agentList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetAgentSelf()
        {
            List<AgentDropdown> agentList = objCasc.BindAgentsSelf(User.Identity.Name);
            //agentList = agentList.Where(x => x.AgenteMail == User.Identity.Name).ToList();
            return Json(agentList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetAgentSelfSeniors()
        {
            List<AgentDropdown> agentList = objCasc.BindAgentsSelfSeniors(User.Identity.Name);
            //agentList = agentList.Where(x => x.AgenteMail == User.Identity.Name).ToList();
            return Json(agentList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetStatus2()
        {
            List<Status> statusList = objCasc.BindStatus2();
            return Json(statusList, JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetDesignations()
        {
            List<Designations> lstDesignations = objCasc.BindDesignations();
            return Json(lstDesignations, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetDesignationsForFranchise()
        {
            List<Designations> lstDesignations = objCasc.BindDesignationsForFranchise();
            return Json(lstDesignations, JsonRequestBehavior.AllowGet);

        }

        //public JsonResult GetOldDesignations()
        //{
        //    List<Designations> lstDesignations = objCasc.BindOldDesignations();
        //    return Json(lstDesignations, JsonRequestBehavior.AllowGet);

        //}

        public JsonResult GetDesignationForDisplay()
        {
            List<Designations> designation = objCasc.GetDesignations(User.Identity.Name);
            return Json(designation.Count <=0?"": designation[0].Designation, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProjects()
        {
            List<Projects> projectList = objBooking.BindProjects();
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllProjects()
        {
            List<Projects> projectList = objBooking.BindAllProjects();
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCityList(int StateId)
        {
            List<City> CityList = objCasc.BindCity(StateId);
            return Json(CityList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetChequeStatus()
        {
            var list = objCasc.BindChequeStatus();
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        public List<State> BindStateDetails(int CountryId)
        {

            List<State> stateDetail = new List<State>();
            try
            {
                stateDetail = objCasc.BindState(CountryId);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return stateDetail;
        }

        public List<City> BindCityDetails(int stateId)
        {
            List<City> cityDetail = new List<City>();
            try
            {
                cityDetail = objCasc.BindCity(stateId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return cityDetail;
        }

        public JsonResult GetPercentages()
        {
            List<GetPercentages> percentageList = objCasc.BindPercentages();
            return Json(percentageList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetApprovalType()
        {
            List<StatusApprove> statusList = new List<StatusApprove>();
            StatusApprove s = new StatusApprove();
            s.ID = "Approved";
            s.Status = "Approved";
            statusList.Add(s);
            StatusApprove s1 = new StatusApprove();
            s1.ID = "Rejected";
            s1.Status = "Rejected";
            statusList.Add(s1);
            StatusApprove s2 = new StatusApprove();
            s2.ID = "Pending";
            s2.Status = "Pending";
            statusList.Add(s2);
            return Json(statusList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReceiptApprovalType()
        {
            List<ReceiptApproval> receiptApproveList = new List<ReceiptApproval>();
            ReceiptApproval s = new ReceiptApproval();
            s.ViewReceipt = true;

            receiptApproveList.Add(s);
            ReceiptApproval s1 = new ReceiptApproval();
            s1.ViewReceipt = false;
            receiptApproveList.Add(s1);
            return Json(receiptApproveList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateAgentLocation(double latitude, double longitude)
        {
            if (User.IsInRole("Agent"))
                objCasc.UpdateAgentLocation(latitude, longitude, User.Identity.Name);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProjectType()
        {
            List<ProjectTypes> lstpType = new List<ProjectTypes>();
            ProjectTypes ptype = new ProjectTypes();
            ptype.ID = 1;
            ptype.ProjectType = "Construction";
            lstpType.Add(ptype);
            ProjectTypes ptype1 = new ProjectTypes();
            ptype1.ID = 1;
            ptype1.ProjectType = "Non-Construction";
            lstpType.Add(ptype1);
            return Json(lstpType, JsonRequestBehavior.AllowGet);
        }
    }
}