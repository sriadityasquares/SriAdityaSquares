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
        public List<GetBookingInfoByDate> BindBookingInfo(DateTime fromDate,DateTime toDate,int projectID)
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
    }
}
