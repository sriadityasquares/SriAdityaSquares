using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using ModelLayer;

namespace BusinessLayer
{
    public class CommonBL
    {
        CommonDL db = new CommonDL();

        public List<Country> BindCountry()
        {
            return db.BindCountry();
        }
        public List<State> BindState(int CountryID)
        {
            return db.BindState(CountryID);
        }

        public List<Area> BindArea(int CityID)
        {
            return db.BindArea(CityID);
        }

        public List<Status> BindStatus()
        {
            return db.BindStatus();
        }

        public List<Cheque> BindChequeStatus()
        {
            return db.BindChequeStatus();
        }
        public List<Agent> BindAgents()
        {
            return db.BindAgents();
        }
        public List<Agent> BindAgentsExceptSAS()
        {
            return db.BindAgentsExceptSAS();
        }
        public List<AgentDropdown> BindAgents2()
        {
            return db.BindAgents2();
        }

        public List<AgentDropdown> BindAgents2Franchise(string username)
        {
            return db.BindAgents2Franchise(username);
        }
        public List<Status> BindStatus2()
        {
            return db.BindStatus2();
        }

        public List<Designations> BindDesignations()
        {
            return db.BindDesignations();
        }

        public List<Designations> BindDesignationsForFranchise()
        {
            return db.BindDesignationsForFranchise();
        }

        public List<Designations> GetDesignations(string username)
        {
            return db.GetDesignations(username);
        }
            public List<City> BindCity(int stateID)
        {
            return db.BindCity(stateID);
        }

        public List<GetPercentages> BindPercentages()
        {
            return db.BindPercentages();
        }

        public List<Year> BindYear()
        {
            return db.BindYear();
        }

        public List<Month> BindMonth()
        {
            return db.BindMonth();
        }

        public void UpdateAgentLocation(double lalitude, double longitude, string email)
        {
            db.UpdateAgentLocation(lalitude, longitude, email);
        }
        public MyProfile GetMyProfile(string username, string role)
        {
            return db.GetMyProfile(username, role);
        }

        public void LogSMS(SMS sms)
        {
            db.LogSMS(sms);
        }

        public List<SMS> GetSMSLogs()
        {
            return db.GetSMSLogs();
        }

        public List<Agent> BindAgentsOnUsername(string username)
        {
            return db.BindAgentsOnUsername(username);
        }
    }
}
