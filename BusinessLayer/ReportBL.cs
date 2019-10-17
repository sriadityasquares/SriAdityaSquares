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
        public List<GetBookingInfoByDate> BindBookingInfo(DateTime start, DateTime end, int projectID)
        {
            return db.BindBookingInfo(start,end,projectID);
        }
    }
}
