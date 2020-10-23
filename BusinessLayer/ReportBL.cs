using DataLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ReportBL
    {
        ReportDL db = new ReportDL();
        public List<GetBookingInfoByDate> BindBookingInfo(string start, string end, string projectID)
        {
            return db.BindBookingInfo(start, end, projectID);
        }

        public List<GetPaymentInfoByDate> BindPaymentInfo(string start, string end, string projectID)
        {
            return db.BindPaymentInfo(start, end, projectID);
        }


        public List<GetAgentWiseBookingDetails> BindAgentBookingInfo(string start, string end, string projectID)
        {
            return db.BindAgentBookingInfo(start, end, projectID);
        }

        public List<GetBhkWiseBookingDetails> BindBhkBookingInfo(string start, string end, string projectID)
        {
            return db.BindBhkBookingInfo(start, end, projectID);
        }

        public List<GetFacingWiseBookingDetails> BindFacingBookingInfo(string start, string end, string projectID)
        {
            return db.BindFacingBookingInfo(start, end, projectID);
        }

        public List<GetFlatWiseTotalAgentCommission> BindFlatAgentCommission(int projectID,int towerID)
        {
            return db.BindFlatAgentCommission(projectID,towerID);
        }

        public List<GetFlatPaymentDetails> GetFlatPayments()
        {
            return db.GetFlatPayments();
        }
        public List<FlatWiseAgentCommission> BindFlatAgentCommissionDetails()
        {
            return db.BindFlatAgentCommissionDetails();
        }

        public List<GetPeriodWiseBookingDetails> BindPeriodWiseBookingInfo(int option, string fromDate, string toDate, string projectID, string years, string month)
        {
            return db.BindPeriodWiseBookingInfo(option, fromDate, toDate, projectID, years, month);
        }

        public List<GetGraphicalPeriodWiseBooking> BindGraphicalPeriodWiseBookingInfo(int option, int projectID, string years, string month)
        {
            return db.BindGraphicalPeriodWiseBookingInfo(option, projectID, years, month);
        }
    }
}
