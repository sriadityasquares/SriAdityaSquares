using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
namespace LoginApp.Controllers
{
    public class CascadeController : Controller
    {
        Common objCasc = new Common();

        // GET: Cascade
        public ActionResult Index()
        {
            List<tblCountry> countryList = objCasc.BindCountry();
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
            List<tblState> StateList = objCasc.BindState(CountryId);
            return Json(StateList, JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetCityList(int StateId)
        {
            List<tblCity> CityList = objCasc.BindCity(StateId);
            return Json(CityList, JsonRequestBehavior.AllowGet);

        }
        public List<tblState> BindStateDetails(int CountryId)
        {

            List<tblState> stateDetail = new List<tblState>();
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

        public List<tblCity> BindCityDetails(int stateId)
        {
            List<tblCity> cityDetail = new List<tblCity>();
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