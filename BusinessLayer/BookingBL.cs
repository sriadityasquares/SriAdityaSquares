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

        public List<Towers> BindTowersInProgress(int projectID)
        {
            return db.BindTowersInProgress(projectID);
        }

        public List<Flats> BindFlatsInProgress(int towerID)
        {
            return db.BindFlatsInProgress(towerID);
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

        public List<PaymentInformation> BindPaymentDetails(int FlatId)
        {
            return db.BindPaymentDetails(FlatId);
        }

        public bool SaveNewPayment(PaymentInformation payInfo)
        {
            return db.SaveNewPayment(payInfo);
        }

        public List<GetTowerDetails> BindTowerDetails()
        {
            return db.BindTowerDetails();
        }

        public List<AgentMaster> BindAgents()
        {
            return db.BindAgents();
        }
    }
}
