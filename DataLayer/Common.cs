using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Common
    {
        salesDBEntities dbEntity = new salesDBEntities();
        public List<tblCountry> BindCountry()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<tblCountry> lstCountry = new List<tblCountry>();
            try
            {
                lstCountry = dbEntity.tblCountries.ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstCountry;
        }

        public List<tblState> BindState(int countryId)
        {
            List<tblState> lstState = new List<tblState>();
            try
            {
                this.dbEntity.Configuration.ProxyCreationEnabled = false;

                lstState = dbEntity.tblStates.Where(a => a.CountryID == countryId).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstState;
        }

        public List<tblCity> BindCity(int stateId)
        {
            List<tblCity> lstCity = new List<tblCity>();
            try
            {
                this.dbEntity.Configuration.ProxyCreationEnabled = false;

                lstCity = dbEntity.tblCities.Where(a => a.StateID == stateId).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstCity;
        }

        public List<Projects> BindProjects()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Projects> lstCountry = new List<Projects>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblProject, Projects>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<List<tblProject>, List<Projects>>(dbEntity.tblProjects.Where(a => a.BookingStatus == 1).ToList()).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstCountry;
        }
    }
}
    

