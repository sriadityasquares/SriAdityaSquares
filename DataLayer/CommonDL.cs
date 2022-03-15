using AutoMapper;
using log4net;
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
        private static readonly ILog log =
           LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                log.Error("Error :" + ex);
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
                lstState = mapper.Map<List<tblState>, List<State>>(dbEntity.tblStates.Where(x => x.CountryID == countryId).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstState;
        }

        public List<Area> BindArea(int cityID)
        {
            List<Area> lstArea = new List<Area>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblArea, Area>();
                });
                IMapper mapper = config.CreateMapper();
                lstArea = mapper.Map<List<tblArea>, List<Area>>(dbEntity.tblAreas.Where(x => x.CityID == cityID).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstArea;
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
                log.Error("Error :" + ex);
            }
            return lstStatus;
        }

        public List<Cheque> BindChequeStatus()
        {
            List<Cheque> lstStatus = new List<Cheque>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblChequeStatu, Cheque>();
                });
                IMapper mapper = config.CreateMapper();
                lstStatus = mapper.Map<List<tblChequeStatu>, List<Cheque>>(dbEntity.tblChequeStatus.ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
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
                lstAgent = mapper.Map<List<tblAgentMaster>, List<Agent>>(dbEntity.tblAgentMasters.Where(x => x.AgentStatus == "A").ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstAgent;
        }

        public List<Agent> BindAgentsExceptSAS()
        {
            List<Agent> lstAgent = new List<Agent>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblAgentMaster, Agent>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgent = mapper.Map<List<tblAgentMaster>, List<Agent>>(dbEntity.tblAgentMasters.Where(x => x.AgentStatus == "A" && x.AgentID != 1).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
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
                lstAgent = mapper.Map<List<tblAgentMaster>, List<AgentDropdown>>(dbEntity.tblAgentMasters.Where(x => x.AgentStatus == "A").OrderBy(x => x.AgentName).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstAgent;
        }

        public List<AgentDropdown> BindAgentsSelf(string user)
        {
            List<AgentDropdown> lstAgent = new List<AgentDropdown>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblAgentMaster, AgentDropdown>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgent = mapper.Map<List<tblAgentMaster>, List<AgentDropdown>>(dbEntity.tblAgentMasters.Where(x => x.AgentStatus == "A" && x.AgenteMail == user).OrderBy(x => x.AgentName).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstAgent;
        }

        public List<AgentDropdown> BindAgentsSelfSeniors(string user)
        {
            List<AgentDropdown> lstAgent = new List<AgentDropdown>();

            var agent = dbEntity.tblAgentMasters.Where(x => x.AgentStatus == "A" && x.AgenteMail == user).FirstOrDefault();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblAgentMaster, AgentDropdown>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgent = mapper.Map<List<tblAgentMaster>, List<AgentDropdown>>(dbEntity.tblAgentMasters.Where(x => x.AgentStatus == "A" && x.AgentCode == agent.AgentSponserCode).OrderBy(x => x.AgentName).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstAgent;
        }


        public List<AgentDropdown> BindAgents2Franchise(string username)
        {
            List<AgentDropdown> lstAgent = new List<AgentDropdown>();
            try
            {
                var franchiseID = dbEntity.tblAgentMasters.Where(x => x.AgenteMail == username).Select(x => x.FranchiseID).FirstOrDefault();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblAgentMaster, AgentDropdown>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgent = mapper.Map<List<tblAgentMaster>, List<AgentDropdown>>(dbEntity.tblAgentMasters.Where(x => x.AgentStatus == "A" && x.FranchiseID == franchiseID).OrderBy(x => x.AgentName).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
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
                log.Error("Error :" + ex);
            }
            return lstStatus;
        }


        public List<Designations> BindDesignations()
        {
            List<Designations> lstDesignations = new List<Designations>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblDesignation, Designations>();
                });
                IMapper mapper = config.CreateMapper();
                lstDesignations = mapper.Map<List<tblDesignation>, List<Designations>>(dbEntity.tblDesignations.Where(x => x.Active == true).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstDesignations;
        }

        public List<Designations> BindDesignationsForFranchise()
        {
            List<Designations> lstDesignations = new List<Designations>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblDesignation, Designations>();
                });
                IMapper mapper = config.CreateMapper();
                lstDesignations = mapper.Map<List<tblDesignation>, List<Designations>>(dbEntity.tblDesignations.Where(x => x.Active == true && x.ConstructionPercentage >=4 && x.ConstructionPercentage <=6.75).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstDesignations;
        }


        public List<Designations> GetDesignations(string username)
        {
            List<Designations> lstDesignations = new List<Designations>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetDesignations_Result, Designations>();
                });
                IMapper mapper = config.CreateMapper();
                lstDesignations = mapper.Map<List<sp_GetDesignations_Result>, List<Designations>>(dbEntity.sp_GetDesignations(username).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstDesignations;

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
                log.Error("Error :" + ex);
            }
            return lstCity;
        }

        public List<GetPercentages> BindPercentages()
        {
            List<GetPercentages> lstPercentage = new List<GetPercentages>();
            try
            {

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetPercentages_Result, GetPercentages>();
                });
                IMapper mapper = config.CreateMapper();
                lstPercentage = mapper.Map<List<sp_GetPercentages_Result>, List<GetPercentages>>(dbEntity.sp_GetPercentages().ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstPercentage;
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
                log.Error("Error :" + ex);
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
                log.Error("Error :" + ex);
            }
            return lstMonth;
        }

        public void UpdateAgentLocation(double lalitude, double longitude, string email)
        {
            try
            {
                var agent = dbEntity.tblAgentMasters.Where(x => x.AgenteMail == email).FirstOrDefault();
                if (agent != null)
                {
                    agent.AgentLatitude = lalitude;
                    agent.AgentLongitude = longitude;
                    dbEntity.SaveChanges();
                }
            }
            catch
            {

            }
        }

        public MyProfile GetMyProfile(string username, string role)
        {
            MyProfile myProfile = new MyProfile();
            try
            {

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetProfile_Result, MyProfile>();
                });
                IMapper mapper = config.CreateMapper();
                myProfile = mapper.Map<sp_GetProfile_Result, MyProfile>(dbEntity.sp_GetProfile(role, username).FirstOrDefault());
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return myProfile;
        }

        public void LogSMS(SMS sms)
        {
            try
            {
                tblSM newSMS = new tblSM();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SMS, tblSM>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<SMS, tblSM>(sms, newSMS);
                dbEntity.tblSMS.Add(newSMS);
                dbEntity.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }

        public List<SMS> GetSMSLogs()
        {
            List<SMS> lstSMS = new List<SMS>();
            try
            {

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblSM, SMS>();
                });
                IMapper mapper = config.CreateMapper();
                lstSMS = mapper.Map<List<tblSM>, List<SMS>>(dbEntity.tblSMS.OrderByDescending(x => x.CreatedDate).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstSMS;
        }

        public List<Agent> BindAgentsOnUsername(string username)
        {
            List<Agent> lstAgent = new List<Agent>();
            try
            {

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblAgentMaster, Agent>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgent = mapper.Map<List<tblAgentMaster>, List<Agent>>(dbEntity.tblAgentMasters.Where(x=>x.AgenteMail == username).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstAgent;

        }


    }
}


