using AutoMapper;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CommonDL
    {
        salesDBEntities dbEntity = new salesDBEntities();
        public List<Country> BindCountry()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Country> lstCountry = new List<Country>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblCountry, Country>();
                });
                IMapper mapper = config.CreateMapper();
                lstCountry = mapper.Map<List<tblCountry>, List<Country>>(dbEntity.tblCountries.ToList()).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstCountry;
        }

        public List<State> BindState(int countryId)
        {
            List<State> lstState = new List<State>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblState, State>();
                });
                IMapper mapper = config.CreateMapper();
                lstState = mapper.Map<List<tblState>, List<State>>(dbEntity.tblStates.Where(x=> x.CountryID == countryId).ToList()).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstState;
        }

        public List<City> BindCity(int stateId)
        {
            List<City> lstCity = new List<City>();
            try
            {

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblCity, City>();
                });
                IMapper mapper = config.CreateMapper();
                lstCity = mapper.Map<List<tblCity>, List<City>>(dbEntity.tblCities.Where(a => a.StateID == stateId).ToList()).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstCity;
        }

        
    }
}
    

