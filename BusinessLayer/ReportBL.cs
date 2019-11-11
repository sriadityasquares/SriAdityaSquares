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
        public List<GetPeriodWiseBookingDetails> BindPeriodWiseBookingInfo(int option, string fromDate, string toDate, string projectID, string years, string month)
        {
            return db.BindPeriodWiseBookingInfo(option, fromDate, toDate, projectID, years, month);
        }
    }
}
