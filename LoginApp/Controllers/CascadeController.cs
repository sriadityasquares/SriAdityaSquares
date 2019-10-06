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
        
        // GET: Cascade
        public ActionResult Index()
        {
            List<Country> countryList = objCasc.BindCountry();
            ViewBag.CountryList = new SelectList(countryList, "CountryID", "CountryName");
            ViewBag.ProjectList = new SelectList(countryList, "CountryID", "CountryName");
            ViewBag.AgentList = new SelectList(countryList, "CountryID", "CountryName");
            return View();
        }

        [HttpGet]
        public IEnumerable<SelectListItem> BindCountryDetails()
        {


            //List<tblCountry> countryDetail = new List<tblCountry>();
            //try
            //{
            //    countryDetail = objCasc.BindCountry();
            //}
            
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            //return countryDetail;
            var li = new List<SelectListItem>();
            li.Add(new SelectListItem
            {
                Text = "--Select--",
                Value = ""
            });


            li.AddRange(objCasc.BindCountry().Select(x => new SelectListItem
            {
                Text = x.CountryName,
                Value = x.CountryID.ToString(),
                Selected = (x.CountryID == 0)
            })) ;
            return li;
        }


        public JsonResult GetStateList(int CountryId)
        {
            List<State> StateList = objCasc.BindState(CountryId);
            return Json(StateList, JsonRequestBehavior.AllowGet);

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