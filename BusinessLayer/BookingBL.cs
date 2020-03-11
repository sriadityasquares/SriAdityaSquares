using DataLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BookingBL
    {
        BookingDL db = new BookingDL();
        public List<Projects> BindProjects()
        {
            return db.BindProjects();
        }

        public List<Projects> BindAllProjects()
        {
            return db.BindAllProjects();
        }

        public List<Towers> BindTowers(int projectID)
        {
            return db.BindTowers(projectID);
        }

        public List<Flats> BindFlats(int towerID)
        {
            return db.BindFlats(towerID);
        }

        public List<Flats> BindAllFlats(int towerID)
        {
            return db.BindAllFlats(towerID);
        }

        public List<Flats> BindAllFlats()
        {
            return db.BindAllFlats();
        }
        public List<Towers> BindTowersInProgress(int projectID)
        {
            return db.BindTowersInProgress(projectID);
        }

        public List<Flats> BindFlatsInProgress(int towerID)
        {
            return db.BindFlatsInProgress(towerID);
        }

        public List<Flats> BindFlatsExceptOpen(int towerID)
        {
            return db.BindFlatsExceptOpen(towerID);
        }

        public List<AgentProjectLevel> BindProjectAgents(int projectID)
        {
            return db.BindProjectAgents(projectID);
        }

        public List<Schemes> BindSchemes(int projectID)
        {
            return db.BindSchemes(projectID);
        }

        public List<FlatDetails> BindFlatDetails(int FlatId, int ProjectID)
        {
            return db.BindFlatDetails(FlatId, ProjectID);
        }

        public bool SaveNewBooking(BookingInformation b)
        {
            return db.SaveNewBooking(b);
        }

        public bool UpdateBooking(BookingInformation b)
        {
            return db.UpdateBooking(b);
        }

        public List<PaymentInformation> BindPaymentDetails(int FlatId)
        {
            return db.BindPaymentDetails(FlatId);
        }

        public List<AgentPaymentInformation> BindAgentPaymentDetails(int FlatId)
        {
            return db.BindAgentPaymentDetails(FlatId);
        }

        public bool SaveNewPayment(PaymentInformation payInfo)
        {
            return db.SaveNewPayment(payInfo);
        }

        public bool SaveNewAgentPayment(AgentPaymentInformation payInfo)
        {
            return db.SaveNewAgentPayment(payInfo);
        }

        public bool SaveNewAgentTotalPayment(AgentTotalPayments payInfo)
        {
            return db.SaveNewAgentTotalPayment(payInfo);
        }
        public List<GetTowerDetails> BindTowerDetails()
        {
            return db.BindTowerDetails();
        }

        public List<Schemes> BindSchemeDetails()
        {
            return db.BindSchemeDetails();
        }
        public List<AgentMaster> BindAgents()
        {
            return db.BindAgents();
        }

        public List<FlatWiseAgentCommission> BindAgentDashboard(string email)
        {
            return db.BindAgentDashboard(email);
        }

        public List<GetFlatLifeCycle> BindFlatLifeCycle(int flatID)
        {
            return db.BindFlatLifeCycle(flatID);
        }
        public List<FlatWiseAgentCommission> BindAgentsDashboard(string email)
        {
            return db.BindAgentsDashboard(email);
        }

        public BookingInformation GetBookingInformation(int FlatID)
        {
            return db.GetBookingInformation(FlatID);
        }

        public List<AgentProjectLevel> BindAgentProjectLevels()
        {
            return db.BindAgentProjectLevels();
        }

        public List<LevelsMaster> BindLevelsMasters()
        {
            return db.BindLevelsMasters();
        }

        public AgentTotalPayments GetAgentTotalPay(int AgentID)
        {
            return db.GetAgentTotalPay(AgentID);
        }
    }
}
