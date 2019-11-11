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

        public JsonResult GetStatus()
        {
            List<Status> statusList = objCasc.BindStatus();
            return Json(statusList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetProjects()
        {
            List<Projects> projectList = objBooking.BindProjects();
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCityList(int StateId)
        {
            List<City> CityList = objCasc.BindCity(StateId);
            return Json(CityList, JsonRequestBehavior.AllowGet);

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
    }
}