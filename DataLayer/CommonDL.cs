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

        public List<Status> BindStatus()
        {
            List<Status> lstStatus = new List<Status>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblStatu, Status>();
                });
                IMapper mapper = config.CreateMapper();
                lstStatus = mapper.Map<List<tblStatu>, List<Status>>(dbEntity.tblStatus.Where(x => x.StatusType == 1).ToList()).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstStatus;
        }

        public List<Agent> BindAgents()
        {
            List<Agent> lstAgent = new List<Agent>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblAgentMaster, Agent>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgent = mapper.Map<List<tblAgentMaster>, List<Agent>>(dbEntity.tblAgentMasters.ToList()).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstAgent;
        }

        public List<AgentDropdown> BindAgents2()
        {
            List<AgentDropdown> lstAgent = new List<AgentDropdown>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblAgentMaster, AgentDropdown>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgent = mapper.Map<List<tblAgentMaster>, List<AgentDropdown>>(dbEntity.tblAgentMasters.ToList()).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstAgent;
        }

        public List<Status> BindStatus2()
        {
            List<Status> lstStatus = new List<Status>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblStatu, Status>();
                });
                IMapper mapper = config.CreateMapper();
                lstStatus = mapper.Map<List<tblStatu>, List<Status>>(dbEntity.tblStatus.Where(x => x.StatusType == 2).ToList()).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstStatus;
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

        public List<Year> BindYear()
        {
            List<Year> lstYear = new List<Year>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblYear, Year>();
                });
                IMapper mapper = config.CreateMapper();
                lstYear = mapper.Map<List<tblYear>, List<Year>>(dbEntity.tblYears.ToList()).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstYear;
        }

        public List<Month> BindMonth()
        {
            List<Month> lstMonth = new List<Month>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblMonth, Month>();
                });
                IMapper mapper = config.CreateMapper();
                lstMonth = mapper.Map<List<tblMonth>, List<Month>>(dbEntity.tblMonths.ToList()).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstMonth;
        }


    }
}
    

