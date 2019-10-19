using AutoMapper;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ReportDL
    {
        salesDBEntities dbEntity = new salesDBEntities();
        public List<GetBookingInfoByDate> BindBookingInfo(string fromDate,string toDate,string projectID)
        {
            List<GetBookingInfoByDate> lstBooking = new List<GetBookingInfoByDate>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_getBookingInfoByDates_Result, GetBookingInfoByDate>();
                });
                IMapper mapper = config.CreateMapper();
                lstBooking = mapper.Map<List<sp_getBookingInfoByDates_Result>, List<GetBookingInfoByDate>>(dbEntity.sp_getBookingInfoByDates(fromDate,toDate,projectID).ToList()).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstBooking;
        }
       

        public List<GetPeriodWiseBookingDetails> BindPeriodWiseBookingInfo(int option,string fromDate, string toDate, string projectID,string years,string month)
        {
            List<GetPeriodWiseBookingDetails> lstBooking = new List<GetPeriodWiseBookingDetails>();
            try
            {
                var list = dbEntity.sp_GetPeriodWiseBookingDetails(1, "1,2", "2018,2019", "", "", "");

                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetPeriodWiseBookingDetails_Result, GetPeriodWiseBookingDetails>();
                });
                IMapper mapper = config.CreateMapper();
                lstBooking = mapper.Map<List<sp_GetPeriodWiseBookingDetails_Result>, List<GetPeriodWiseBookingDetails>>(dbEntity.sp_GetPeriodWiseBookingDetails(option,projectID, years,month,fromDate.ToString(),toDate.ToString()).ToList()).ToList();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lstBooking;
        }
    }
}
